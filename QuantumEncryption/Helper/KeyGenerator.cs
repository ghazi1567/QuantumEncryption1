using QuantumEncryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace QuantumEncryption.Helper
{
    public static class KeyGenerator
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
        private static void CalculatePrimeNumbers(Int64 highestNumber)
        {
            Int64 p1 = GetPrimeNumber(highestNumber);
            p = new BigInteger(p1);
            Int64 p2 = GetPrimeNumber(p1 - 1);
            q = new BigInteger(p2);
        }

        public static Key GetKey(Int64 number)
        {
            CalculatePrimeNumbers(number);
            n = n_value(p, q);
            BigInteger n_phi = cal_phi(p, q);
            e = e_value(p, q);
            d = cal_privateKey(n_phi, e);

            return new Key
            {
                PublicKey = $"{e},{n}",
                PrivateKey = $"{d},{n}",
               P = $"{p}",
                Q = $"{q}"
            };
        }

    }
}
