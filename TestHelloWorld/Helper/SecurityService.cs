using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Helper
{
    public class SecurityService
    {


        #region Cryptography Service

        //// Public Methods ////

        // Encryption 

        public string DecryptSecret(string secret)//, out string returnValue)
        {
            //returnValue = String.Empty;
            try
            {
                var returnValue = Decrypt(secret ?? "");
                return returnValue;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }
        public static string EncryptSecret(string secretSalt, string secret)//, out string returnValue)
        {
            string returnValue;
            //returnValue = String.Empty;
            if (!string.IsNullOrEmpty(secret))
            {
                try
                {
#pragma warning disable CA1062 // Validate arguments of public methods
                    returnValue = Encrypt(secretSalt.ToString(), secret.ToString());
#pragma warning restore CA1062 // Validate arguments of public methods
                    return returnValue;
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw e.InnerException;

                    throw;
                }
            }
            return string.Empty;

        }
        public string GetSecretSalt()
        {
            //returnValue = String.Empty;
            try
            {
                var returnValue = GetRandomSecretSalt();
                return returnValue;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }

        }

        public static bool DecryptAndCheck(string clearText, string salt, string encryptedSecret)
        {
            try
            {
                string encCode = EncryptSecret(salt, clearText);
                string midPhase1 = Decrypt(encryptedSecret ?? "");
                string midPhase2 = Decrypt(encCode);
                string hexSalt = GetHexString(salt ?? "");
#pragma warning disable CA1307 // Specify StringComparison
                return string.CompareOrdinal(midPhase1.Replace(hexSalt, string.Empty), midPhase2.Replace(hexSalt, string.Empty)) == 0;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        //// Private Methods ////
        private static string GetRandomSecretSalt()
        {
            try
            {
                const int saltLength = 10;
                const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()_+|";

                var random = new Random();
                var sb = new StringBuilder(saltLength);
                for (int i = 0; i < saltLength; i++)
                {
                    var randomNumber = random.Next(0, characters.Length);
                    sb.Append(characters, randomNumber, 1);
                }
                var returnValue = sb.ToString();
                return returnValue;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        private static string Encrypt(string secretSalt, string clearSecret)
        {
            try
            {
                secretSalt = GetHexString(secretSalt);
                clearSecret = GetHexString(clearSecret);

                HMACMD5 hash = new HMACMD5();

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
                foreach (byte bt in bytes)
                {
#pragma warning disable CA1305 // Specify IFormatProvider
                    sb.AppendFormat("{0:x2}", bt);
#pragma warning restore CA1305 // Specify IFormatProvider
                }
                var returnValue = sb.ToString();
                return returnValue;

            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }

        private static string GetHexString(string input)
        {
            try
            {
                char[] values = input.ToCharArray();
                string hexOutput = string.Empty;
                foreach (char letter in values)
#pragma warning disable CA1305 // Specify IFormatProvider
                    hexOutput = String.Concat(hexOutput, String.Format("{0:X}", Convert.ToInt32(letter)));
#pragma warning restore CA1305 // Specify IFormatProvider
                return hexOutput;
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;
                throw;
            }
        }

        private static string Decrypt(string encryptedSecret)
        {
            try
            {
                int numberChars = encryptedSecret.Length;
                byte[] bytes = new byte[numberChars / 2];
                for (int i = 0; i < numberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(encryptedSecret.Substring(i, 2), 16);
                }

                return System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    throw e.InnerException;

                throw;
            }
        }
        #endregion


    }
}
