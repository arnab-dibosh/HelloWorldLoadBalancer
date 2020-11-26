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
using System.Security.Cryptography;
using Cryptography.Services;

namespace TestHelloWorld.Controllers
{
    [ApiController]
    public class DynamicSqlController : ControllerBase
    {
        private readonly ILogger<DynamicSqlController> _logger;        
        public DynamicSqlController(ILogger<DynamicSqlController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/OneInsertDynamicSqlTran", Name = "OneInsertDynamicSqlTran")]
        public string OneInsertDynamicSqlTran(string transactionId, string clientRequestTime)
        {
            string retVal = transactionId;
            try
            {
                DBUtility.OneInsertDynamicSqlNoLog(transactionId, "", "", 1, "", "", 2, 1, DateTime.Now, clientRequestTime, "OneInsertDynamicSqlTran");             
                return "Direct Pay Successfull";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("/OneInsertDynamicSqlWithLogTran", Name = "OneInsertDynamicSqlWithLogTran")]
        public string OneInsertDynamicSqlWithLogTran(string transactionId, string clientRequestTime) {
            string retVal = transactionId;
            try {
                DBUtility.OneInsertDynamicSqlWithLog(transactionId, "", "", 1, "", "", 2, 1, DateTime.Now, clientRequestTime, "OneInsertDynamicSqlTran", _logger);
                return "Direct Pay Successfull";
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [HttpPost("/TwoGetOneInsertDynamicSqlTran", Name = "TwoGetOneInsertDynamicSqlTran")]
        public IActionResult TwoGetOneInsertDynamicSqlTran(string transactionId, string clientRequestTime) {
            try {
                string sendervid = "arnab@user.idtp", receiverVid = "nesar@user.idtp";
                decimal amount = 2;
                var senderInfo = DBUtility.GetUser(sendervid);
                var receiverInfo = DBUtility.GetUser(receiverVid);
                DBUtility.OneInsertDynamicSqlNoLog(transactionId, sendervid, senderInfo.AccountNo, senderInfo.BankId, 
                    receiverVid, receiverInfo.AccountNo, receiverInfo.BankId, amount, DateTime.Now, clientRequestTime, "TwoGetOneInsertDynamicSqlTran");

                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex) {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/TwoGetOneInsertDynamicSqlXmlTran", Name = "TwoGetOneInsertDynamicSqlXmlTran")]
        public IActionResult TwoGetOneInsertDynamicSqlXmlTran([FromBody]Payload payload)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
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

                var senderInfo = DBUtility.GetUser(sendervid);
                var receiverInfo = DBUtility.GetUser(receiverVid);
                DBUtility.OneInsertDynamicSqlNoLog(payload.transactionId, sendervid, senderInfo.AccountNo, senderInfo.BankId,
                    receiverVid, receiverInfo.AccountNo, receiverInfo.BankId, amount, DateTime.Now, payload.clientRequestTime, "TwoGetOneInsertDynamicSqlXmlTran");

                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/TwoGetOneInsertDynamicSqlXmlDecryptTran", Name = "TwoGetOneInsertDynamicSqlXmlDecryptTran")]
        public IActionResult TwoGetOneInsertDynamicSqlXmlDecryptTran([FromBody] Payload payload) {
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

                var senderInfo = DBUtility.GetUser(sendervid);
                var receiverInfo = DBUtility.GetUser(receiverVid);
                DBUtility.OneInsertDynamicSqlNoLog(payload.transactionId, sendervid, senderInfo.AccountNo, senderInfo.BankId,
                    receiverVid, receiverInfo.AccountNo, receiverInfo.BankId, amount, DateTime.Now, payload.clientRequestTime, "TwoGetOneInsertDynamicSqlXmlDecryptTran");

                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex) {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

    }
}
