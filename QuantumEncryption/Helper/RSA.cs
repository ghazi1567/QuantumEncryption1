using Microsoft.AspNetCore.Identity;
using QuantumEncryption.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuantumEncryption.Helper
{
    public static class RSA
    {
        static BigInteger p = 99999999999999997;//99991;
        static BigInteger q = 99999999999999977;//99989;
        public static BigInteger n = 0;
        static BigInteger e = 0;
        public static BigInteger d = 0;
         static BigInteger n_value(BigInteger prime1, BigInteger prime2)
        {
            return (prime1 * prime2);
        }
         static BigInteger cal_phi(BigInteger prime1, BigInteger prime2)
        {
            return ((prime1 - 1) * (prime2 - 1));
        }
         static BigInteger e_value(BigInteger p, BigInteger q)
        {
            BigInteger e = 23;
            BigInteger phi = (p - 1) * (q - 1);
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
         static BigInteger gcd(BigInteger a, BigInteger h)
        {
            BigInteger temp;
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
         static BigInteger cal_privateKey(BigInteger phi, BigInteger e)
        {


            BigInteger ca = phi;
            BigInteger cb = e;

            BigInteger u = BigInteger.One;
            BigInteger v = BigInteger.Zero;
            BigInteger s = v;
            BigInteger t = u;

            while (!cb.IsZero)
            {
                BigInteger quotient, remainder;

                quotient = ca / cb; //quotient is 1
                remainder = ca % cb; //remainder is 2
                //BigInteger dd = ca.divmod(cb);
                BigInteger na = cb;

                BigInteger nb = remainder;

                BigInteger nu = s;
                BigInteger nv = t;

                BigInteger ns = BigInteger.Subtract(u, BigInteger.Multiply(quotient, s));
                BigInteger nt = BigInteger.Subtract(v, BigInteger.Multiply(quotient, t));

                ca = na;
                cb = nb;

                u = nu;
                v = nv;
                s = ns;
                t = nt;

            }
            if (v < 0)
            {
                v = BigInteger.Add(v, phi);
            }
            return v;
            //let private_key = gg.v;
            //if (private_key.lesser(0))
            //{
            //    private_key = private_key.add(phi);
            //          var res = new {
            //              a: ca, u: u, v: v,
            //s: s, t: t
            //      }

        }
        static BigInteger Encrypted_Number(BigInteger integer, BigInteger e, BigInteger n)
        {
            if (integer < n)
            {
                return BigInteger.ModPow(integer, e, n);
            }
            throw new Exception("The integer must be less than the value of n in order to be decypherable!");
        }

        static BigInteger Decrypt_Number(BigInteger integer, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(integer, d, n);
        }

        public static Int64 GetPrimeNumber(Int64 aa)
        {
            for (Int64 i = aa; i >= 0; i--)
            {
                if (IsPrime(i))
                {
                    return i;
                }
                
            }
            return aa;
        }
        public static bool IsPrime(Int64 number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (Int64)Math.Floor(Math.Sqrt(number));

            for (Int64 i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
        private  static void CalculatePrimeNumbers(string userKey)
        {
            Int64 _bignum = 99999999999999997;
            int userKeyNumber = Convert.ToInt32(userKey, 2);
            


            Int64 highestNumber = _bignum + userKeyNumber;
            Int64 p1 = GetPrimeNumber(highestNumber);
            p = new BigInteger(p1);
            Int64 p2 = GetPrimeNumber(p1-1);
            q = new BigInteger(p2);
        }

        public static Key GetPublicKey(string userKey)
        {
            CalculatePrimeNumbers(userKey);
            n = n_value(p, q);
            BigInteger n_phi = cal_phi(p, q);
            e = e_value(p, q);
            d = cal_privateKey(n_phi, e);
       
            return new Key
            {
                PublicKey = $"{e},{n}",
                PrivateKey  = $"{d},{n}",
            };
        }



        public static string StartEncryption(string str,string publicKey)
        {
            string[] arr = publicKey.Split(',');

            StringBuilder sb = new StringBuilder();
            foreach (var item in Split(str,10))
            {
                sb.Append($"{Encrypted_Number(new BigInteger(Encoding.UTF8.GetBytes(item)),BigInteger.Parse(arr[0]), BigInteger.Parse(arr[1]))}|");
            }
            return sb.ToString();
        }
        public static string StartDecryption(string str,string privateKey)
        {
            string[] arr = privateKey.Split(',');

            StringBuilder sb = new StringBuilder();
            foreach (var item in str.Split('|'))
            {
                if (!string.IsNullOrEmpty(item))
                {
                    BigInteger a = BigInteger.Parse(item);
                    BigInteger decryptedInteger2 = Decrypt_Number(a,BigInteger.Parse(arr[0]), BigInteger.Parse(arr[1]));
                    string decryptedIntegerAsString = Encoding.UTF8.GetString(decryptedInteger2.ToByteArray());
                    sb.Append($"{decryptedIntegerAsString}");
                }
            }
            return sb.ToString();
        }
        static IEnumerable<string> Split(string str, int chunkSize)
        {
            if (str.Length < chunkSize)
            {
                return Enumerable.Range(0, 1)
                .Select(i => str);
            }

            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }



    }
}
