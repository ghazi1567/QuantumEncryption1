using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.API.Helper
{
    public class encrypt
    {
        //  Call this function to remove the key from memory after use for security
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        static string EncryptFile(FileStream fsInput,
          string sOutputFilename,
          string sKey)
        {
          

            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //Assign For Encryption
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Starts to encrypt a file
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            //send the converted stream to Output File 
            CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
           
            fsEncrypted.Close(); 
            string fileContents;
            using (StreamReader reader = new StreamReader(sOutputFilename))
            {
                fileContents = reader.ReadToEnd();
            }
            return fileContents;
        }

        static byte[] EncryptFile(byte[] bytearrayinput,
       string sOutputFilename,
       string sKey)
        {

            var data = Encoding.UTF8.GetString(bytearrayinput);

            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //Assign For Encryption
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Starts to encrypt a file
            var res = "";
            //send the converted stream to Output File 
            using (ICryptoTransform desencrypt = DES.CreateEncryptor())
            {
                byte[] decryptedText = desencrypt.TransformFinalBlock(bytearrayinput, 0, bytearrayinput.Length);

                 res = Encoding.UTF8.GetString(decryptedText);
                return decryptedText;
            }
            //CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt,
            //   CryptoStreamMode.Write);

            //byte[] bytearrayinput = new byte[fsInput.Length];
            //fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            //cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            //cryptostream.Close();
            //fsInput.Close();

            //fsEncrypted.Close();
            //string fileContents;
            //using (StreamReader reader = new StreamReader(sOutputFilename))
            //{
            //    fileContents = reader.ReadToEnd();
            //}
            //return res;
        }


        static void EncryptText(string sInputFilename,
          string sOutputFilename,
          string sKey)
        {
            FileStream fsInput = new FileStream(sInputFilename,
               FileMode.Open,
               FileAccess.Read);

            FileStream fsEncrypted = new FileStream(sOutputFilename,
               FileMode.Create,
               FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //Assign For Encryption
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Starts to encrypt a file
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            //send the converted stream to Output File 
            CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt,
               CryptoStreamMode.Write);

            byte[] bytearrayinput = new byte[fsInput.Length];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }
        public string crypt(string t, string txtplain)
        {
            // Must be 64 bits, 8 bytes.
            // Distribute this key to the user who will decrypt this file.
            string sSecretKey;

            // Get the Key for the file to Encrypt.
            sSecretKey = t;
            string input = txtplain;

            string enc = txtplain + "_encrypt";

            GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);

            // Encrypt the file.        
           // EncryptFile(txtplain, @enc, sSecretKey);

            // Remove the Key from memory. 
            ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
            gch.Free();
            return enc;
        }
        public string crypt(string t, FileStream file)
        {
            // Must be 64 bits, 8 bytes.
            // Distribute this key to the user who will decrypt this file.
            string sSecretKey;

            // Get the Key for the file to Encrypt.
            sSecretKey = t;

            GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);
            var filePath = Path.GetTempFileName();
            // Encrypt the file.        
            var fileContent= EncryptFile(file, filePath, sSecretKey);

            // Remove the Key from memory. 
            ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
            gch.Free();
            return fileContent;
        }

        public byte[] crypt(string t, byte[] file)
        {
            // Must be 64 bits, 8 bytes.
            // Distribute this key to the user who will decrypt this file.
            string sSecretKey;

            // Get the Key for the file to Encrypt.
            sSecretKey = t;

            GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);
            var filePath = Path.GetTempFileName();
            // Encrypt the file.        
            var fileContent = EncryptFile(file, filePath, sSecretKey);

            // Remove the Key from memory. 
            ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
            gch.Free();
            return fileContent;
        }


        static string DecryptFile(FileStream fsread, string sOutputFilename, string sKey)
        {
            //byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(ss);



            // System.IO.MemoryStream ms=new System.IO.MemoryStream(bytIn,0,bytIn.Length);            

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
           // FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);


            //Print the contents of the decrypted file.           
            //StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            // cryptostreamDecr.Write(bytIn, 0, bytIn.Length);
            //StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            //fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            //fsDecrypted.Flush();
            //fsDecrypted.Close();
            string fileContents;
            using (StreamReader reader = new StreamReader(cryptostreamDecr))
            {
                fileContents = reader.ReadToEnd();
            }
            return fileContents;
        }
        static byte[] DecryptFile(byte[] fsreadbyte, string sOutputFilename, string sKey)
        {
            //byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(ss);

            Stream fsread = new MemoryStream(fsreadbyte);

            // System.IO.MemoryStream ms=new System.IO.MemoryStream(bytIn,0,bytIn.Length);            

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            // FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            using (ICryptoTransform desencrypt = DES.CreateDecryptor())
            {
                byte[] decryptedText = desencrypt.TransformFinalBlock(fsreadbyte, 0, fsreadbyte.Length);

               var res = Encoding.UTF8.GetString(decryptedText);
                return decryptedText;
            }

            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);


            //Print the contents of the decrypted file.           
            //StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            // cryptostreamDecr.Write(bytIn, 0, bytIn.Length);
            //StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            //fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            //fsDecrypted.Flush();
            //fsDecrypted.Close();
            string fileContents;
            using (StreamReader reader = new StreamReader(cryptostreamDecr))
            {
                fileContents = reader.ReadToEnd();
            }
            //return fileContents;
        }

        public string decrypt(string sSecretKey, FileStream file)
        {

            var filePath = Path.GetTempFileName();
         
            GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);
            var dc = DecryptFile(file, filePath, sSecretKey);
            //ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
            gch.Free();
            return dc;

        }
        public byte[] decrypt(string sSecretKey, byte[] file)
        {

            var filePath = Path.GetTempFileName();

            GCHandle gch = GCHandle.Alloc(sSecretKey, GCHandleType.Pinned);
            var dc = DecryptFile(file, filePath, sSecretKey);
            //ZeroMemory(gch.AddrOfPinnedObject(), sSecretKey.Length * 2);
            gch.Free();
            return dc;

        }


        public string Encrypt(string clearText, string sKey)
        {
           
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //Assign For Encryption
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
  
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, DES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }

            return clearText;
        }

        public string Decrypt(string cipherText, string sKey)
        {
        
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //Assign For Encryption
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);


                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, DES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            
            return cipherText;
        }
    }
}
