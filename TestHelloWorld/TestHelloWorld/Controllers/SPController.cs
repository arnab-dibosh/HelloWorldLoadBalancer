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
    [ApiController]
    public class SPController : ControllerBase
    {

        private readonly ILogger<SPController> _logger;
        public SPController(ILogger<SPController> logger) {
            _logger = logger;
        }


        [HttpPost("/OneInsertWithSpTran", Name = "OneInsertWithSpTran")]
        public string OneInsertWithSpTran([FromBody] Payload payload) {
            
            try {
                DBUtility.OneInsertSpTran(payload.transactionId, payload.clientRequestTime, "SP_OneInsertWithSpTran");
                return "Direct Pay Successfull";
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPost("/TwoGetOneInsertSpTran", Name = "TwoGetOneInsertSpTran")]
        public IActionResult TwoGetOneInsertSpTran([FromBody] Payload payload) {
            try {
                string sendervid = "arnab@user.idtp", receiverVid = "nesar@user.idtp";
                decimal amount = 2;
                DBUtility.TwoGetOneInsertSpTran(sendervid, receiverVid, amount, payload.transactionId, payload.clientRequestTime, "SP_TwoGetOneInsertSpTran");

                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex) {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/TwoGetOneInsertSPXmlTran", Name = "TwoGetOneInsertSPXmlTran")]
        public IActionResult TwoGetOneInsertSPXmlTran([FromBody] Payload payload) {
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

                DBUtility.TwoGetOneInsertSpTran(sendervid, receiverVid, amount, payload.transactionId, payload.clientRequestTime, "SP_TwoGetOneInsertSPXmlTran");

                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex) {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/TwoGetOneInsertSPXmlDecryptTran", Name = "TwoGetOneInsertSPXmlDecryptTran")]
        public IActionResult TwoGetOneInsertSPXmlDecryptTran([FromBody] Payload payload) {
            XmlDocument doc = new XmlDocument();
            try {
                doc.LoadXml(payload.xmlData);

                RSA idtpPrivate = RSA.Create();
                RSA idtpPublic = RSA.Create();
                RSA sampleBankPrivate = RSA.Create();
                RSA sampleBankPublic = RSA.Create();

                sampleBankPrivate.FromXmlString(IDTPCryptography.bankPrivateKey);
                idtpPublic.FromXmlString(IDTPCryptography.idtpPublicKey);
                IDTPCryptography cryptography = new IDTPCryptography();
                doc = cryptography.DecryptAndValidateSignature(doc, sampleBankPrivate, idtpPublic);

                string sendervid = "", receiverVid = "", idtppin = "";

                sendervid = doc.GetElementsByTagName("DbtrAcct").Item(0).InnerText;
                receiverVid = doc.GetElementsByTagName("CdtrAcct").Item(0).InnerText;
                idtppin = doc.GetElementsByTagName("IDTP_PIN").Item(0).InnerText;
                string amountstr = "";
                amountstr = doc.GetElementsByTagName("IntrBkSttlmAmt").Item(0).InnerText;

                if (!idtppin.Equals("123456")) throw new Exception("Invalid Pin");

                decimal amount = 0;
                decimal.TryParse(amountstr, out amount);

                DBUtility.TwoGetOneInsertSpTran(sendervid, receiverVid, amount, payload.transactionId, payload.clientRequestTime, "SP_TwoGetOneInsertSPXmlDecryptTran");

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
                RSA idtpPrivate = RSA.Create();
                RSA sampleBankPublic = RSA.Create();
                IDTPCryptography cryptography = new IDTPCryptography();
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

    }
}
