using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHelloWorld.Model;

namespace TestHelloWorld.Controllers
{
    [ApiController]
    public class SecurityController : ControllerBase
    {
        // From Jamil Bhai 12-Jan-2021
        [HttpPost("/DecryptDataWithSignature", Name = "DecryptDataWithSignature")]
        public string DecryptDataWithSignature([FromBody] SucurityPayload payload)
        {
            var key = "b14ca5898a4e4133bbce2ea2315a1916"; //32-bytes
            var nonce = "b14ca5898a4e"; //12-bytes
            var assocdata = "done";//DateTime.Now.ToString("yyyyMMddHHmmssfff");
            
            try
            {
                var idtpCrypto = new IDTPSecurity.IDTPCryptoServices();

                byte[] encryptedText = Convert.FromBase64String(payload.xmlData);
                byte[] tagHMAC = Convert.FromBase64String(payload.signatureData);

                byte[] resultDecryption =
                        idtpCrypto.Decrypt(
                            encryptedText,
                            Encoding.UTF8.GetBytes(key),
                            Encoding.UTF8.GetBytes(nonce),
                            tagHMAC,
                            Encoding.UTF8.GetBytes(assocdata));

                byte[] decryptedText = resultDecryption;

                var strDecrypData = System.Text.Encoding.Default.GetString(decryptedText);
                //DBUtility.OneInsertSpTran(payload.transactionId, payload.clientRequestTime, "SP_OneInsertWithSpTran");
                return strDecrypData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/EncryptDataAndSignature", Name = "EncryptDataAndSignature")]
        public string EncryptDataAndSignature([FromBody] PayloadXml payload)
        {
            var key = "b14ca5898a4e4133bbce2ea2315a1916"; //32-bytes
            var nonce = "b14ca5898a4e"; //12-bytes
            var assocdata = "done";//DateTime.Now.ToString("yyyyMMddHHmmssfff");

            try
            {
                var idtpCrypto = new IDTPSecurity.IDTPCryptoServices();

                (byte[] cipherText, byte[] tag) resultEncryption =
                            idtpCrypto.Encrypt(
                                Encoding.UTF8.GetBytes(payload.xmlData),
                                Encoding.UTF8.GetBytes(key),
                                Encoding.UTF8.GetBytes(nonce),
                                Encoding.UTF8.GetBytes(assocdata));

                byte[] encryptedText = resultEncryption.cipherText;
                byte[] tagHMAC = resultEncryption.tag;

                var strEncrypData = "Encrypted Text = " + Convert.ToBase64String(encryptedText) + " Encrypted Signature = " + Convert.ToBase64String(tagHMAC); 
                
                //DBUtility.OneInsertSpTran(payload.transactionId, payload.clientRequestTime, "SP_OneInsertWithSpTran");
                return strEncrypData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/EncryptWithAllSecurityInput", Name = "EncryptWithAllSecurityInput")]
        public string EncryptWithAllSecurityInput([FromBody] SucurityPayloadAll payload)
        {
            //var key = "b14ca5898a4e4133bbce2ea2315a1916"; //32-bytes
            //var nonce = "b14ca5898a4e"; //12-bytes
            //var assocdata = "done";//DateTime.Now.ToString("yyyyMMddHHmmssfff");

            try
            {
                var idtpCrypto = new IDTPSecurity.IDTPCryptoServices();

                (byte[] cipherText, byte[] tag) resultEncryption =
                            idtpCrypto.Encrypt(
                                Encoding.UTF8.GetBytes(payload.plainText),
                                Encoding.UTF8.GetBytes(payload.key32Bytes),
                                Encoding.UTF8.GetBytes(payload.nonce12Bytes),
                                Encoding.UTF8.GetBytes(payload.assocData));

                byte[] encryptedText = resultEncryption.cipherText;
                byte[] tagHMAC = resultEncryption.tag;

                var strEncrypData = "Encrypted Text = " + Convert.ToBase64String(encryptedText) + " Encrypted Signature = " + Convert.ToBase64String(tagHMAC);

                //DBUtility.OneInsertSpTran(payload.transactionId, payload.clientRequestTime, "SP_OneInsertWithSpTran");
                return strEncrypData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/DecryptWithAllSecurityInput", Name = "DecryptWithAllSecurityInput")]
        public string DecryptWithAllSecurityInput([FromBody] SucurityPayloadAll payload)
        {
            //var key = "b14ca5898a4e4133bbce2ea2315a1916"; //32-bytes
            //var nonce = "b14ca5898a4e"; //12-bytes
            //var assocdata = "done";//DateTime.Now.ToString("yyyyMMddHHmmssfff");

            try
            {
                var idtpCrypto = new IDTPSecurity.IDTPCryptoServices();

                byte[] encryptedText = Convert.FromBase64String(payload.plainText);
                byte[] tagHMAC = Convert.FromBase64String(payload.signatureData);

                byte[] resultDecryption =
                        idtpCrypto.Decrypt(
                            encryptedText,
                            Encoding.UTF8.GetBytes(payload.key32Bytes),
                            Encoding.UTF8.GetBytes(payload.nonce12Bytes),
                            tagHMAC,
                            Encoding.UTF8.GetBytes(payload.assocData));

                byte[] decryptedText = resultDecryption;

                var strDecrypData = System.Text.Encoding.Default.GetString(decryptedText);
                //DBUtility.OneInsertSpTran(payload.transactionId, payload.clientRequestTime, "SP_OneInsertWithSpTran");
                return strDecrypData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("/DecryptDataWithDbOperation", Name = "DecryptDataWithDbOperation")]
        public string DecryptDataWithDbOperation([FromBody] SucurityPayload payload)
        {
            var key = "b14ca5898a4e4133bbce2ea2315a1916"; //32-bytes
            var nonce = "b14ca5898a4e"; //12-bytes
            var assocdata = "done";//DateTime.Now.ToString("yyyyMMddHHmmssfff");

            try
            {
                var idtpCrypto = new IDTPSecurity.IDTPCryptoServices();

                byte[] encryptedText = Convert.FromBase64String(payload.xmlData);
                byte[] tagHMAC = Convert.FromBase64String(payload.signatureData);

                byte[] resultDecryption =
                        idtpCrypto.Decrypt(
                            encryptedText,
                            Encoding.UTF8.GetBytes(key),
                            Encoding.UTF8.GetBytes(nonce),
                            tagHMAC,
                            Encoding.UTF8.GetBytes(assocdata));

                byte[] decryptedText = resultDecryption;

                var strDecrypData = System.Text.Encoding.Default.GetString(decryptedText);
                DBUtility.TransferFundFinalSp("AddTransaction_V2");
                return "Insert Successfull";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
