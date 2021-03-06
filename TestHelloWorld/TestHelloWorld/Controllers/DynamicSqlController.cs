﻿using System;
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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using X509_Signature;

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

        [HttpPost("/WriteDataBinary", Name = "WriteDataBinary")]
        public string WriteDataBinary(byte[] bytes) {           
           
            try {
                string xmlData ="";
                BinaryFormatter formatter = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream(bytes)) {
                    xmlData = (string)formatter.Deserialize(ms);
                }
                   
                return "Decode Successfull";
            }
            catch (Exception ex) {
                return ex.Message;
            }
        }

        [HttpPost("/WriteData", Name = "WriteData")]
        public string WriteData(string transactionId, string clientRequestTime)
        {
            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            string retVal = transactionId;
            try
            {

                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try
                {
                    DBUtility.WriteData(transactionId, "Hello-World", strDockerName, clientRequestTime, apiRequestStartTime);
                }
                catch (Exception)
                {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/NoWriteDataPayload", Name = "NoWriteDataPayload")]
        public string NoWriteDataPayload([FromBody] PayloadXml payload) {
          
            try {
                
                return "Insert Successfull";
            }
            catch (Exception) {
                throw;
            }
        }


        [HttpPost("/WriteDataPayload", Name = "WriteDataPayload")]
        public string WriteDataPayload([FromBody] SimplePayload payload)
        {
            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try
            {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try
                {
                    DBUtility.WriteData(payload.transactionId, "Hello-World", strDockerName, payload.clientRequestTime, apiRequestStartTime);
                }
                catch (Exception)
                {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/BaseLineWithXML", Name = "BaseLineWithXML")]
        public string BaseLineWithXML([FromBody] Payload payload)
        {

            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try
            {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try
                {
                    DBUtility.WriteData(payload.transactionId, "Hello-World", strDockerName, payload.clientRequestTime, apiRequestStartTime);
                }
                catch (Exception)
                {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/BaseLineWithJson", Name = "BaseLineWithJson")]
        public string BaseLineWithJson([FromBody] JsonPayload payload)
        {

            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try
            {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try
                {
                    DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello-World", strDockerName, DateTime.Now.ToShortDateString(), apiRequestStartTime);
                }
                catch (Exception)
                {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/BaseLineWithEncryptedJson", Name = "BaseLineWithEncryptedJson")]
        public string BaseLineWithEncryptedJson([FromBody] EncryptedJsonPayload payload)
        {

            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try
            {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try
                {
                    DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello-World", strDockerName, DateTime.Now.ToShortDateString(), apiRequestStartTime);
                }
                catch (Exception)
                {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/BaseLineWithDynamicJson", Name = "BaseLineWithDynamicJson")]
        public string BaseLineWithDynamicJson([FromBody] dynamic payload)
        {

            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try
            {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try
                {
                    DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello-World", strDockerName, DateTime.Now.ToShortDateString(), apiRequestStartTime);
                }
                catch (Exception)
                {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/BaseLineWithDynamicJsonAsync", Name = "BaseLineWithDynamicJsonAsync")]
        public async Task<string> BaseLineWithDynamicJsonAsync([FromBody] dynamic payload)
        {

            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try
            {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try
                {
                    await DBUtility.WriteDataAsync(Guid.NewGuid().ToString(), "Hello-World", strDockerName, DateTime.Now.ToShortDateString(), apiRequestStartTime);
                }
                catch (Exception)
                {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/BaseLineXMLNoInsertion", Name = "BaseLineXMLNoInsertion")]
        public string BaseLineXMLNoInsertion([FromBody] Payload payload)
        {
            return "Insert Successfull";
        }

        [HttpPost("/BaseLineWithXMLString", Name = "BaseLineWithXMLString")]
        public string BaseLineWithXMLString([FromBody] string xmlData)
        {

            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try
            {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try
                {
                    DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello-World", strDockerName, DateTime.Now.ToShortTimeString(), apiRequestStartTime);
                }
                catch (Exception ex)
                {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost("/BaseLineWithXMLDeserialize", Name = "BaseLineWithXMLDeserialize")]
        public string BaseLineWithXMLDeserialize([FromBody] string xmlData) {

            string apiRequestStartTime = "";
            apiRequestStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            try {
                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = string.Empty;
                try {
                    DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello-World", strDockerName, "2020-01-01", apiRequestStartTime);
                }
                catch (Exception) {
                    throw;
                }

                return "Insert Successfull";
            }
            catch (Exception) {
                throw;
            }
        }


        [HttpPost("/OneInsertDynamicSqlTran", Name = "OneInsertDynamicSqlTran")]
        public string OneInsertDynamicSqlTran([FromBody] SimplePayload payload)
        {
            try
            {
                DBUtility.OneInsertDynamicSqlNoLog(payload.transactionId, "arnab", "accountno", 1, "receiver", "accountno",
                    2, 1, DateTime.Now, payload.clientRequestTime, "OneInsertDynamicSqlTran");
                return "Direct Pay Successfull";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost("/OneInsertDynamicSqlWithLogTran", Name = "OneInsertDynamicSqlWithLogTran")]
        public string OneInsertDynamicSqlWithLogTran([FromBody] SimplePayload payload)
        {

            try
            {
                DBUtility.OneInsertDynamicSqlWithLog(payload.transactionId, "", "", 1, "", "", 2, 1, DateTime.Now, payload.clientRequestTime, "OneInsertDynamicSqlTran", _logger);
                return "Direct Pay Successfull";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("/TwoGetOneInsertDynamicSqlTran", Name = "TwoGetOneInsertDynamicSqlTran")]
        public IActionResult TwoGetOneInsertDynamicSqlTran([FromBody] SimplePayload payload)
        {
            try
            {
                string sendervid = "arnab@user.idtp", receiverVid = "nesar@user.idtp";
                decimal amount = 2;
                var senderInfo = DBUtility.GetUser(sendervid);
                var receiverInfo = DBUtility.GetUser(receiverVid);
                DBUtility.OneInsertDynamicSqlNoLog(payload.transactionId, sendervid, senderInfo.AccountNo, senderInfo.BankId,
                    receiverVid, receiverInfo.AccountNo, receiverInfo.BankId, amount, DateTime.Now, payload.clientRequestTime, "TwoGetOneInsertDynamicSqlTran");

                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/TwoGetOneInsertDynamicSqlXmlTran", Name = "TwoGetOneInsertDynamicSqlXmlTran")]
        public IActionResult TwoGetOneInsertDynamicSqlXmlTran([FromBody] Payload payload)
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

        [HttpPost("/TwoGetOneInsertDynamicSqlXmlTranTest", Name = "TwoGetOneInsertDynamicSqlXmlTranTest")]
        public IActionResult TwoGetOneInsertDynamicSqlXmlTranTest([FromBody] string xmlData)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xmlData);

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
                DBUtility.OneInsertDynamicSqlNoLog(Guid.NewGuid().ToString(), sendervid, senderInfo.AccountNo, senderInfo.BankId,
                    receiverVid, receiverInfo.AccountNo, receiverInfo.BankId, amount, DateTime.Now, DateTime.Now.ToShortTimeString(), "TwoGetOneInsertDynamicSqlXmlTran");

                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/TwoGetOneInsertDynamicSqlXmlDecryptTran", Name = "TwoGetOneInsertDynamicSqlXmlDecryptTran")]
        public IActionResult TwoGetOneInsertDynamicSqlXmlDecryptTran([FromBody] Payload payload)
        {
            XmlDocument doc = new XmlDocument();
            try
            {

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
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }


        [HttpPost("/TwoGetOneInsertDynamicSqlConstantDecryptTran", Name = "TwoGetOneInsertDynamicSqlConstantDecryptTran")]
        public IActionResult TwoGetOneInsertDynamicSqlConstantDecryptTran([FromBody] SimplePayload payload)
        {
            XmlDocument doc = new XmlDocument();
            try
            {

                doc.LoadXml(Constants.XMLData);

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
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/XMLDecryptAndValidationWithOpenSSL", Name = "XMLDecryptAndValidationWithOpenSSL")]
        public IActionResult XMLDecryptAndValidationWithOpenSSL([FromBody] Payload payload)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(payload.xmlData);
                AESEncryptDecrypt.DecryptXml(doc);
                var isValidated = X509_Signature.X509_Signature.ValidateSignedinXmlDocument(doc.OuterXml);
                if (isValidated)
                {
                    string sendervid = "", receiverVid = "";

                    sendervid = doc.GetElementsByTagName("DbtrAcct").Item(0).InnerText;
                    receiverVid = doc.GetElementsByTagName("CdtrAcct").Item(0).InnerText;
                    string amountstr = "";
                    amountstr = doc.GetElementsByTagName("IntrBkSttlmAmt").Item(0).InnerText;

                    string strDockerName = System.Environment.MachineName;
                    string dtApiResponseTime = DateTime.Now.ToShortDateString();
                    try
                    {
                        DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello-World", strDockerName, dtApiResponseTime, dtApiResponseTime);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
                }
                else
                {
                    return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = "XML validation failed" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

        [HttpPost("/XMLDecryptAndValidationWithExistingAlgo", Name = "XMLDecryptAndValidationWithExistingAlgo")]
        public IActionResult XMLDecryptAndValidationWithExistingAlgo([FromBody] Payload payload)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(payload.xmlData);

                RSA idtpPrivate = RSA.Create();
                RSA idtpPublic = RSA.Create();
                RSA sampleBankPrivate = RSA.Create();
                RSA sampleBankPublic = RSA.Create();

                sampleBankPrivate.FromXmlString(IDTPCryptography.bankPrivateKey);
                idtpPublic.FromXmlString(IDTPCryptography.idtpPublicKey);
                IDTPCryptography cryptography = new IDTPCryptography();
                doc = cryptography.DecryptAndValidateSignature(doc, sampleBankPrivate, idtpPublic);

                string sendervid = "", receiverVid = "";

                sendervid = doc.GetElementsByTagName("DbtrAcct").Item(0).InnerText;
                receiverVid = doc.GetElementsByTagName("CdtrAcct").Item(0).InnerText;
                string amountstr = "";
                amountstr = doc.GetElementsByTagName("IntrBkSttlmAmt").Item(0).InnerText;

                string strDockerName = System.Environment.MachineName;
                string dtApiResponseTime = DateTime.Now.ToShortDateString();
                try
                {
                    DBUtility.WriteData(Guid.NewGuid().ToString(), "Hello-World", strDockerName, dtApiResponseTime, dtApiResponseTime);
                }
                catch (Exception)
                {
                    throw;
                }

                return new JsonResult(new { StatusCode = HttpStatusCode.OK, Message = "Direct Pay Successfull" });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { StatusCode = HttpStatusCode.BadRequest, Message = ex.Message });
            }
        }

    }
}
