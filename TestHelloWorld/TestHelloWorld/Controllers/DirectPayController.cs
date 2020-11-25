using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Xml;
using System.Net;
using TestHelloWorld.Model;
using Cryptography.Services;
using System.Security.Cryptography;
using System.IO;

//using StackExchange.Redis;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestHelloWorld.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class DirectPayController : ControllerBase
    {

        private readonly ILogger<DirectPayController> _logger;
        private readonly TestDbContext _context;
        IDTPCryptography cryptography = new IDTPCryptography();

        RSA idtpPrivate = RSA.Create();
        RSA idtpPublic = RSA.Create();
        RSA sampleBankPrivate = RSA.Create();
        RSA sampleBankPublic = RSA.Create();
        public DirectPayController(ILogger<DirectPayController> logger, TestDbContext context) {
            _context = context;
            _logger = logger;
        }

        [HttpPost("/DirectPay", Name = "DirectPay")]
        public IActionResult DirectPay(Payload payload, string transactionId, string clientRequestTime) {
            XmlDocument doc = new XmlDocument();

            try {
                doc.LoadXml(payload.xmlData);
                
                string sendervid = "", receiverVid = "", idtppin = "";

                sendervid = doc.GetElementsByTagName("DbtrAcct").Item(0).InnerText;
                receiverVid = doc.GetElementsByTagName("CdtrAcct").Item(0).InnerText;
                idtppin = doc.GetElementsByTagName("IDTP_PIN").Item(0).InnerText;
                string amountstr = "";
                amountstr = doc.GetElementsByTagName("IntrBkSttlmAmt").Item(0).InnerText;

                if (!idtppin.Equals("123456")) throw new Exception("Invalid Pin");

                decimal amount = 0;
                decimal.TryParse(amountstr, out amount);

                decimal availBalance = 0;
                DBUtility.ProcessDirectPayAsync(sendervid, receiverVid, amount, transactionId, clientRequestTime, _logger);


                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex) {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/DirectPayEncrypted", Name = "DirectPayEncrypted")]
        public IActionResult DirectPayEncrypted(Payload payload, string transactionId, string clientRequestTime) {
            XmlDocument doc = new XmlDocument();
            
            try {
                doc.LoadXml(payload.xmlData);

                sampleBankPrivate.FromXmlString(IDTPCryptography.bankPrivateKey);
                idtpPublic.FromXmlString(IDTPCryptography.idtpPublicKey);
                doc = cryptography.DecryptAndValidateSignature(doc, sampleBankPrivate, idtpPublic);

                string sendervid = "", receiverVid = "", idtppin="";

                sendervid = doc.GetElementsByTagName("DbtrAcct").Item(0).InnerText;
                receiverVid = doc.GetElementsByTagName("CdtrAcct").Item(0).InnerText;
                idtppin = doc.GetElementsByTagName("IDTP_PIN").Item(0).InnerText;
                string amountstr = "";
                amountstr = doc.GetElementsByTagName("IntrBkSttlmAmt").Item(0).InnerText;

                if (!idtppin.Equals("123456")) throw new Exception("Invalid Pin");

                decimal amount = 0;
                decimal.TryParse(amountstr, out amount);

                 decimal availBalance = 0;
                DBUtility.ProcessDirectPayAsync(sendervid, receiverVid, amount, transactionId, clientRequestTime, _logger);

                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex) {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/Encrypt", Name = "Encrypt")]
        public string Encrypt([FromBody]string xmlData) {
            XmlDocument doc = new XmlDocument();
            string enccryptedxml = string.Empty;
            try {
                doc.LoadXml(xmlData);
                                
                string idtpPrivateKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Security", "idtpPrivate.key");
                string bankPublicKeyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Security", "sampleBank1Public.pub");

                idtpPrivate.FromXmlString(System.IO.File.ReadAllText(idtpPrivateKeyPath));
                sampleBankPublic.FromXmlString(System.IO.File.ReadAllText(bankPublicKeyPath));
                enccryptedxml = cryptography.SingAndEncryptDocument(doc, idtpPrivate, sampleBankPublic).InnerXml;
            }
            catch(Exception ex) {
                enccryptedxml = ex.Message;
            }
                
            return enccryptedxml;
        }

        [HttpGet("/TestOperation", Name = "TestOperation")]
        public IEnumerable<Bank> TestOperation() {
            var banks = _context.Banks.ToList();
            return banks;
        }


    }
}
