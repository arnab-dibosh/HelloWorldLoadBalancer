using IDTP.Security;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Cryptography.Services
{
    public class IDTPCryptography
    {
        public static string idtpPublicKey = "<RSAKeyValue><Modulus>4VdXTnGQIgPRZtpM6hFnA1qq+2OFvW8aBzKZY+/xybsFQFJWpAGgybn5b7JuCCwXQfioKknp9+MFX5yesSTQAK5hvlljdDaz7DjB03uJpmNyJ6dY2VZLopbPzdhYMmt5JRoie49YULno0uv5Imtt4/a+HSuZ84ShjS9fSOrhbdbXiVsBtZ6Q+gy17+E4UIOUd2fYPj3YpetUx0hp/bJ7OeUf6aZBYj0F4Sfe7uBVnn2XCBMUd96Weh68fyWyYWDo9FIJalARuWDJgnED58HItoWpEblyWzeymSO42UxO2ISv4y2t6Tyw2VhpoDSbpy0b6WvspksAwny3B4Ug6pJv0Q==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static string bankPrivateKey = "<RSAKeyValue><Modulus>5VHNthgX6WyEkKLI+eABaBZ7TJoTpds0dioUydVzc9rYfCkLhjuIqYABhNggHrxkqBZXdQSSjOtwL8DCibtddeE+es3ySgpEvnNDtAIOZsSoOD+h5LQTKJjOWickkkqE/kXE3D327ybUgiw7zx+SAInhP30x6LKKocndIAqvFbCXXOJ/9YiZJMWUcMRhMofAHgdPrRrqvAMLeyOF7+zActtonjQnKnnsMeBDISuMwPuyflDSGxqKcnOoBiwevhT/NaIm9XEwSbJY3xyH2KEuWPRWEbo0SBZcs32mVUhK29JqsEufVQcW+YzZFO++Gyv+3Lnf6M+Qwp/Nla5yTT/MuQ==</Modulus><Exponent>AQAB</Exponent><P>8wG7d0J7ZzvBSeMYarLLPmWmGyJFU25GUV9mmSAorixm1MqK7kXySMPPdR5vOvElN4EwjzTCF2RTSrw/RqEROf6YG4wwAngrpzBCgJkyDpNUOOpw+EisJeEM3urccmzH3ti3iufJTS27PlW8pt4ucrczXplHq5Hi+6iyzz2vf4s=</P><Q>8ZS4mPZKY7hOd/2EmYYupaP/vSdvMsvV1KtAQanZ08PRaHnsYpF0modLDzq+26JPfHKesdqH+V2Ngkvz4tIqaBC3qaM1WGoZo2LWLiqxE0lT0XDKwmCX7/mzOVn7Vc5gHc3c3UyggGRO+4Lv+6fAvty/G694+LsEgngxazF+LUs=</Q><DP>sZhagVY9Cb1KDc3CUnhIwVlYhAwPzmGRUnb+bHpsQJ6CqJE959WGtFcmjtmnQNRr9pyb+Iz+LEpN9RiOxfeAt3mxgLB7hdJ9VLqYX5OWWcCilNYBqfKynRxUY7YRVi7aA6suuH2dSKJbbbsLHIjUOVJaYJB1KJZ95J3t0DeswvE=</DP><DQ>g8fi4t4HOlqBw4ax3KWLsKPDj8WBLS5wGLDKPoeO4avCAcHwptw9xUXkNgyPrHPf9Dz6QH67CdZ0qQ0RnzuSEm58Ibd8FBzcm3oA5/I14hVv+aITmPDkMO3/TFu/dNE9MUTpJHb2XtNZGm4Uxyx9QiBcx5dy4Av0q88w9g5ri/0=</DQ><InverseQ>AZoHYlyVVvqjB4RpBuhhx4Noz/NpFKhtEVuKz3A1iT/0a8zxASOk/iRUUxkHmPwicKHgUnVMFRyPKvxIWqt/ETWs9ECQ1z3T+iJJrNkUyv0cIAhQc1nXYgBf8azhq/bPhk9azoMTXeooYCgow89K7qRUhTGdQiCxEyQu2cNNR2E=</InverseQ><D>CzGOCAhvCl7YrbK0erX7e5g1VozizqK/kdEGCMAZjZsuHAlo3ZmEVzm/WTuvfbCWfTnx9O1PNf+8DyiqlkyGCF4BTb9Fx2Vu65j2wg+jolKRH4XDokVD9iRXVkE44McwbLT9If8IUa4ki2IbXUXeO5Z/Xzj+OayVZcZTu9+pojZP1a3IWH9tzyTg7VZujGPnTTtgt4Jk/45EvtPnCse/W2MZl5zHHXD9az5VzK9LmHdRb3NZoCkB96ODewgu+QNZL6ivyxoCxbSmEQHTE9cfBuY2f+b2itEa0jOWfqjvR2ZqTqEZ2Ma0sY/HtApWi1dXcekBxelUk352qm3Agl+CHQ==</D></RSAKeyValue>";
        MessageSigningHelper helper = MessageSigningHelper.Create();
        public string DecryptSecret(string secret)//, out string returnValue)
        {
            string returnValue;
            //returnValue = String.Empty;
            try {
                returnValue = Decrypt(secret ?? "");
                return returnValue;
            }
            catch (Exception e) {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }


        public string EncryptSecret(string secretSalt, string secret)//, out string returnValue)
        {
            string returnValue;
            //returnValue = String.Empty;
            if (!string.IsNullOrEmpty(secret)) {
                try {
#pragma warning disable CA1062 // Validate arguments of public methods
                    returnValue = Encrypt(secretSalt.ToString(), secret.ToString());
#pragma warning restore CA1062 // Validate arguments of public methods
                    return returnValue;
                }
                catch (Exception e) {
                    if (e.InnerException != null)
                        throw e.InnerException;

                    throw;
                }
            }
            return string.Empty;

        }


        public string GetSecretSalt() {

            string returnValue;
            //returnValue = String.Empty;
            try {
                returnValue = GetRandomSecretSalt();
                return returnValue;
            }
            catch (Exception e) {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }

        }

        private static string GetRandomSecretSalt() {
            try {
                int saltLength = 10;
                string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()_+|";
                string returnValue = string.Empty;

                Random random = new Random();
                StringBuilder sb = new StringBuilder(saltLength);
                for (int i = 0; i < saltLength; i++) {
                    int randomNumber = random.Next(0, characters.Length);
                    sb.Append(characters, randomNumber, 1);
                }
                returnValue = sb.ToString();
                return returnValue;
            }
            catch (Exception e) {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        private static string Encrypt(string secretSalt, string clearSecret) {
            try {
                string returnValue = string.Empty;

                secretSalt = GetHexString(secretSalt);
                clearSecret = GetHexString(clearSecret);

                System.Security.Cryptography.HMACMD5 hash = new System.Security.Cryptography.HMACMD5();

                byte[] returnBytes = new byte[secretSalt.Length / 2];
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(secretSalt.Substring(i * 2, 2), 16);
                hash.Key = returnBytes;

                string encodedSecret = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(clearSecret)));

#pragma warning disable CA1305 // Specify IFormatProvider
                string newSecret = string.Format("{0}{1}", secretSalt, encodedSecret);
#pragma warning restore CA1305 // Specify IFormatProvider
                byte[] bytes = Encoding.UTF8.GetBytes(newSecret);
                StringBuilder sb = new StringBuilder();
                foreach (byte bt in bytes) {
#pragma warning disable CA1305 // Specify IFormatProvider
                    sb.AppendFormat("{0:x2}", bt);
#pragma warning restore CA1305 // Specify IFormatProvider
                }
                returnValue = sb.ToString();
                return returnValue;

            }
            catch (Exception e) {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        private static string GetHexString(string input) {
            try {
                char[] values = input.ToCharArray();
                string hexOutput = string.Empty;
                foreach (char letter in values)
#pragma warning disable CA1305 // Specify IFormatProvider
                    hexOutput = String.Concat(hexOutput, String.Format("{0:X}", Convert.ToInt32(letter)));
#pragma warning restore CA1305 // Specify IFormatProvider
                return hexOutput;
            }
            catch (Exception e) {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        private static string Decrypt(string encryptedSecret) {
            try {
                string returnValue = string.Empty;

                int NumberChars = encryptedSecret.Length;
                byte[] bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2) {
                    bytes[i / 2] = Convert.ToByte(encryptedSecret.Substring(i, 2), 16);
                }

                return returnValue = System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch (Exception e) {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
            //return returnValue.Substring(10);
        }

        public bool DecryptAndCheck(string clearText, string salt, string encryptedSecret) {
            try {
                string encCode = EncryptSecret(salt, clearText);
                string midPhase1 = Decrypt(encryptedSecret ?? "");
                string midPhase2 = Decrypt(encCode);
                string hexsalt = GetHexString(salt ?? "");
#pragma warning disable CA1307 // Specify StringComparison
                if (String.Compare(midPhase1.Replace(hexsalt, string.Empty), midPhase2.Replace(hexsalt, string.Empty)) == 0)
#pragma warning restore CA1307 // Specify StringComparison
                    return true;
                return false;//.Substring(10);
            }
            catch (Exception e) {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        #region Message Security
        public XmlDocument SingAndEncryptDocument(XmlDocument xmlDocument, RSA signingKey, RSA encryptionKey) {
            try {
                //XmlDocument doc = new XmlDocument();
                //doc.Load(xmlDocument.OuterXml);
                // First create a content from xml to create a digest from
                string messageContent = helper.CreateMessageContentFromXmlDocument(xmlDocument.InnerXml);
                // Now create digest
                byte[] messageDigest = helper.GetMessageHash(messageContent);
                // Now sign the digest with sender's private key
                byte[] signedDigest = helper.SignDigest(messageDigest, signingKey);
                // Now put the elements in the xml
                helper.CreateXmlSignature(xmlDocument, messageDigest, signedDigest);
                // now encrypt the xml with receiver's public key 
                helper.EncryptXmlMessage(xmlDocument, encryptionKey);
                return xmlDocument;
            }
            catch (Exception ex) {
                throw ex.InnerException;
            }
        }

        public XmlDocument DecryptAndValidateSignature(XmlDocument xmlDocument, RSA decryptionKey, RSA signingKey) {
            try {
                //XmlDocument doc = new XmlDocument();
                //doc.Load(xmlDocument.OuterXml);
                // Now decrypt the contents with receiver's private key

                try {
                    helper.DecryptXmlMessage(xmlDocument, decryptionKey);
                }
                catch (Exception ex) {

                    throw;
                }

                // Now read the digest values from the doc 
                var signature = helper.ReadXmlSignature(xmlDocument);
                string messageContent2 = helper.CreateMessageContentFromXmlDocument(xmlDocument.InnerXml);
                byte[] messageDigest2 = helper.GetMessageHash(messageContent2);
                if (Convert.ToBase64String(messageDigest2).Equals(Convert.ToBase64String(signature.Digest))) {
                    if (helper.VerifySignature(messageDigest2, signature.Signature, signingKey) == false) {
                        throw new InvalidDataException("Message origin could not be verified!");
                    }
                }
                else {
                    throw new InvalidDataException("Digest does not match! Data might be tampered!");
                }
                return xmlDocument;
            }
            catch (Exception ex) {
                throw ex.InnerException;
            }
        }
        #endregion




    }
}
