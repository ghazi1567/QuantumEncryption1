using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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
        static public byte[] Encryption(byte[] file, string key)
        {
            try
            {
                var data  = Encoding.UTF8.GetString(file);
                Int32 aa = Convert.ToInt32(key);
                int y = key.Length;
                long t = hashing(aa, y);
             
                encrypt er = new encrypt();
                 var  en_file = er.crypt(t.ToString("D8"), file);
                var enc = er.Encrypt(data, t.ToString("D8"));
                var dec = er.Decrypt(enc, t.ToString("D8")); 
              
                //return encryptedData;
                return en_file;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }
        static public string EncryptionStr(byte[] file, string key)
        {
            try
            {
                var data = Encoding.UTF8.GetString(file);
                Int32 aa = Convert.ToInt32(key);
                int y = key.Length;
                long t = hashing(aa, y);

                encrypt er = new encrypt();
                var enc = er.Encrypt(data, t.ToString("D8"));
                var dec = er.Decrypt(enc, t.ToString("D8"));

                //return encryptedData;
                return enc;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }
        static public string Decryptionstr(byte[] file, string key)
        {
            try
            {
                var data = Encoding.UTF8.GetString(file);
                Int32 aa = Convert.ToInt32(key);
                int y = key.Length;
                long t = hashing(aa, y);
                var finalKey = t.ToString();
                encrypt er = new encrypt();
                var en_file = er.Decrypt(data, t.ToString("D8"));
                return en_file;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
        static public byte[] Decryption(byte[] file, string key)
        {
            try
            {
                Int32 aa = Convert.ToInt32(key);
                int y = key.Length;
                long t = hashing(aa, y);
                var finalKey = t.ToString();
                encrypt er = new encrypt();
                var en_file = er.decrypt(finalKey, file);
                return en_file;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
        static public string Encryption_old(string txtplain, string key)
        {
            try
            {
              
                Int32 aa = Convert.ToInt32(key);
                int y = key.Length;
                long t = hashing(aa, y);
                var finalKey = t.ToString();
                //encrypt er = new encrypt();
                //en_file = er.crypt(keys, files);

                RSAParameters RSAKey = RSACryptoService.ExportParameters(false);

                var i = GetBigInteger(RSAKey.Exponent);
                var p = GetBigInteger(RSAKey.Modulus);

                RSAParameters RSAKey1 = getKey(173, 211);
                RSACryptoService.ImportParameters(RSAKey1);
                
                byte[] Data = ByteConverter.GetBytes(txtplain);
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedData = RSA.Encrypt(Data, false);
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
        static public string Decryption_old(string encryptedText, string key)
        {
            try
            {
                RSACryptoService.ImportParameters(getKey(3, 11));
                byte[] Data = ByteConverter.GetBytes(encryptedText);
                RSAParameters RSAKey = RSACryptoService.ExportParameters(true);
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, false);
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
        public static long hashing(int a, int y)
        {
            Int64 QK;
            Int64 ins = Convert.ToInt64(y);
            if (y == 0)
            {
                Random ee = new Random();
                y = ee.Next(100);
            }
            //Random rnd1 = new Random();
            Int16 aa = 52;
            //Random rnd2=new Random();
            Int16 bb = 3232;
            QK = y * aa * bb;
            string q = QK.ToString();
            if (q.Length == 8)
            {
                return QK;
            }
            else
            {
            x: for (int i = 0; i <= q.Length; i++)
                {
                    if (q.Length > 8)
                    {
                        q = q.Substring(0, 8);
                        ins = Convert.ToInt64(q);
                    }
                    if (q.Length == 8)
                    {
                        return ins;
                    }
                    if (q.Length == 7)
                    {
                        QK = ins * aa * bb * 7;
                        q = QK.ToString();
                        ins = Convert.ToInt64(QK);
                    }
                    if (q.Length == 6)
                    {
                        QK = ins * aa * bb * 9;
                        q = QK.ToString();
                        ins = Convert.ToInt64(QK);
                    }
                    if (q.Length == 5)
                    {
                        QK = ins * aa * bb * 11;
                        q = QK.ToString();
                        ins = Convert.ToInt64(QK);
                    }
                    if (q.Length == 4)
                    {
                        QK = ins * aa * bb * 13;
                        q = QK.ToString();
                        ins = Convert.ToInt64(QK);
                    }
                    if (q.Length == 3)
                    {
                        QK = ins * aa * bb * 15;
                        q = QK.ToString();
                        ins = Convert.ToInt64(QK);
                    }
                    if (q.Length == 2)
                    {
                        QK = ins * aa * bb * 17;
                        q = QK.ToString();
                        ins = Convert.ToInt64(QK);
                    }

                }
                if (q.Length == 8)
                {
                    return ins;
                }
                else
                {
                    goto x;
                }
            }
            return ins;
        }






        public static long square(long a)
        {
            return (a * a);
        }

        public static long BigMod(int b, int p, int m) //b^p%m=?
        {
            if (p == 0)
                return 1;
            else if (p % 2 == 0)
                return square(BigMod(b, p / 2, m)) % m;
            else
                return ((b % m) * BigMod(b, p - 1, m)) % m;
        }

        public static int n_value(int prime1, int prime2)
        {
            return (prime1 * prime2);
        }
        public static int e_value(int p, int q)
        {
            int e = 2;
            int phi = (p - 1) * (q - 1);
            while (e < phi)
            {
                // e must be co-prime to phi and 
                // smaller than phi. 
                if (gcd(e, phi) == 1)
                    break;
                else
                    e++;
            }
            return e;
        }
        private static int gcd(int a, int h)
        {
            int temp;
            while (true)
            {
                temp = a % h;
                if (temp == 0)
                    return h;
                a = h;
                h = temp;
            }
            return 0;
        }
        public static int cal_phi(int prime1, int prime2)
        {
            return ((prime1 - 1) * (prime2 - 1));
        }

        public static BigInteger cal_privateKey(BigInteger phi, BigInteger e, BigInteger n)
        {
            BigInteger d = 0;
            BigInteger RES = 0;

            for (d = 1; ; d++)
            {
                RES = (d * e) % phi;
                if (RES == 1) break;
            }
            return d;
        }

        private static BigInteger ModInverse(BigInteger e, BigInteger n)
        {
            BigInteger r = n;
            BigInteger newR = e;
            BigInteger t = 0;
            BigInteger newT = 1;

            while (newR != 0)
            {
                BigInteger quotient = r / newR;
                BigInteger temp;

                temp = t;
                t = newT;
                newT = temp - quotient * newT;

                temp = r;
                r = newR;
                newR = temp - quotient * newR;
            }

            if (t < 0)
            {
                t = t + n;
            }

            return t;
        }
        public static RSAParameters getKey(int p, int q)
        {

            BigInteger n = n_value(p, q);
            BigInteger n_phi = cal_phi(p, q);
            BigInteger e = e_value(p, q);
            BigInteger d = cal_privateKey(n_phi, e, n);
            BigInteger dp = d % (p - BigInteger.One);
            BigInteger dq = d % (q - BigInteger.One);
            BigInteger inverseQ = ModInverse(q, p);


            byte[] rndBuf = n.ToByteArray();

            if (rndBuf[rndBuf.Length - 1] == 0)
            {
                rndBuf = new byte[rndBuf.Length - 1];
            }

            int modLen = rndBuf.Length;
            int halfModLen = (modLen + 1) / 2;

            ////value.ToByteArray();
            //return new RSAParameters
            //{
            //    Modulus = n.ToByteArray(),
            //    Exponent = e.ToByteArray(),
            //    D = d.ToByteArray(),
            //    P = BigInteger.Parse(p.ToString()).ToByteArray(),
            //    Q = BigInteger.Parse(q.ToString()).ToByteArray(),
            //    DP = dp.ToByteArray(),
            //    DQ = dq.ToByteArray(),
            //    InverseQ = inverseQ.ToByteArray(),
            //};
            var aa = RecoverRSAParameters(n, e, d);


            var result = new RSAParameters
            {
                Modulus = GetBytes(n, modLen),
                Exponent = GetBytes(e, -1),
                D = GetBytes(d, modLen),
                P = GetBytes(p, halfModLen),
                Q = GetBytes(q, halfModLen),
                DP = GetBytes(dp, halfModLen),
                DQ = GetBytes(dq, halfModLen),
                InverseQ = GetBytes(inverseQ, halfModLen),
            };

            return aa;
        }


        private static BigInteger GetBigInteger(byte[] bytes)
        {
            byte[] signPadded = new byte[bytes.Length + 1];
            Buffer.BlockCopy(bytes, 0, signPadded, 1, bytes.Length);
            Array.Reverse(signPadded);
            return new BigInteger(signPadded);
        }
        private static byte[] GetBytes(BigInteger value, int size)
        {
            byte[] bytes = value.ToByteArray();

            if (size == -1)
            {
                size = bytes.Length;
            }

            if (bytes.Length > size + 1)
            {
                throw new InvalidOperationException($"Cannot squeeze value {value} to {size} bytes from {bytes.Length}.");
            }

            if (bytes.Length == size + 1 && bytes[bytes.Length - 1] != 0)
            {
                throw new InvalidOperationException($"Cannot squeeze value {value} to {size} bytes from {bytes.Length}.");
            }

            Array.Resize(ref bytes, size);
            Array.Reverse(bytes);
            return bytes;
        }
        private static RSAParameters RecoverRSAParameters(BigInteger n, BigInteger e, BigInteger d)
        {
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                BigInteger k = d * e - 1;

                if (!k.IsEven)
                {
                    throw new InvalidOperationException("d*e - 1 is odd");
                }

                BigInteger two = 2;
                BigInteger t = BigInteger.One;

                BigInteger r = k / two;

                while (r.IsEven)
                {
                    t++;
                    r /= two;
                }

                byte[] rndBuf = n.ToByteArray();

                if (rndBuf[rndBuf.Length - 1] == 0)
                {
                    rndBuf = new byte[rndBuf.Length - 1];
                }

                BigInteger nMinusOne = n - BigInteger.One;

                bool cracked = false;
                BigInteger y = BigInteger.Zero;

                for (int i = 0; i < 100 && !cracked; i++)
                {
                    BigInteger g;

                    do
                    {
                        rng.GetBytes(rndBuf);
                        g = GetBigInteger(rndBuf);
                    }
                    while (g >= n);

                    y = BigInteger.ModPow(g, r, n);

                    if (y.IsOne || y == nMinusOne)
                    {
                        i--;
                        continue;
                    }

                    for (BigInteger j = BigInteger.One; j < t; j++)
                    {
                        BigInteger x = BigInteger.ModPow(y, two, n);

                        if (x.IsOne)
                        {
                            cracked = true;
                            break;
                        }

                        if (x == nMinusOne)
                        {
                            break;
                        }

                        y = x;
                    }
                }

                if (!cracked)
                {
                    throw new InvalidOperationException("Prime factors not found");
                }

                BigInteger p = BigInteger.GreatestCommonDivisor(y - BigInteger.One, n);
                BigInteger q = n / p;
                BigInteger dp = d % (p - BigInteger.One);
                BigInteger dq = d % (q - BigInteger.One);
                BigInteger inverseQ = ModInverse(q, p);

                int modLen = rndBuf.Length;
                int halfModLen = (modLen + 1) / 2;

                return new RSAParameters
                {
                    Modulus = GetBytes(n, modLen),
                    Exponent = GetBytes(e, -1),
                    D = GetBytes(d, modLen),
                    P = GetBytes(p, halfModLen),
                    Q = GetBytes(q, halfModLen),
                    DP = GetBytes(dp, halfModLen),
                    DQ = GetBytes(dq, halfModLen),
                    InverseQ = GetBytes(inverseQ, halfModLen),
                };
            }
        }
    }
}
