using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.API.Helper
{
    public static class RSA
    {
        static UnicodeEncoding ByteConverter = new UnicodeEncoding();
        static RSACryptoServiceProvider RSACryptoService = new RSACryptoServiceProvider();
        #region-----Encryptionand Decryption Function-----
        static public string Encryption(string txtplain, bool DoOAEPPadding)
        {
            try
            {
                RSAParameters RSAKey = RSACryptoService.ExportParameters(false);
                byte[] Data = ByteConverter.GetBytes(txtplain);
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                //return encryptedData;
                return ByteConverter.GetString(encryptedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }
        static public string Decryption(string encryptedText, bool DoOAEPPadding)
        {
            try
            {
                byte[] Data = ByteConverter.GetBytes(encryptedText);
                RSAParameters RSAKey = RSACryptoService.ExportParameters(true);
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                //return decryptedData;
                return ByteConverter.GetString(decryptedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
        static public string Decryption(byte[] Data, bool DoOAEPPadding)
        {
            try
            {
                //byte[] Data = ByteConverter.GetBytes(encryptedText);
                RSAParameters RSAKey = RSACryptoService.ExportParameters(true);
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                //return decryptedData;
                return ByteConverter.GetString(decryptedData);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
        #endregion
    }
}
