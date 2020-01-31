using System;
using System.Collections.Generic;
using System.Text;

namespace Quantum.API.Helper
{
    public class SessionKey
    {
        long p, q;
        public SessionKey(long p1, long  p2)
        {
            p = p1;
            q = p2;
        }
        long great, a;
        double aa, bb, cc, rm;
        long rd;

        public long calE(long pi, long p, long q)
        {
            great = 0;
            aa = Math.Log(pi) / Math.Log(10);
            bb = Math.Floor(aa);
            cc = Math.Pow(10, bb);
            Random r = new Random();
            double dr = r.Next(1000);
            rm = dr * cc;
            string sss = "" + Math.Round(rm);
            rd = Convert.ToInt64(sss);
            while (great != 1)
            {
                rd = rd + 1;
                great = GCD(rd, pi);
                pi = (p - 1) * (q - 1);
            }
            return rd;
        }
        //End of  CalE

        long GCD(long e, long pi)
        {
            if (e > pi)
            {
                while (e % pi != 0)
                {
                    a = e % pi;
                    e = pi;
                    pi = a;
                }
                great = pi;
            }
            else
            {
                while (pi % e != 0)
                {
                    a = pi % e;
                    pi = e;
                    e = a;
                }
                great = e;
            }
            return great;
        }
        //End of GCD

        public string getSessionKey()
        {

            long  pi, e, val, ds, r, qd;
            long d, n;
            int i, cnt;
            long[] rst = new long[100];
            long[] div = new long[100];
            long[] qud = new long[100];
            long[] rem = new long[100];
            string fe = "";
            string fd = "";
            string fn = "";
            // End of variables declaration

            Random rnd1 = new Random(10);
            Random rnd2 = new Random(10);
            
            //p = prime1.longValue();
            //q = prime2.longValue();
          

            n = p * q;
            pi = (p - 1) * (q - 1);
            e = calE(pi, p, q);


            qd = pi / e;
            r = pi % e;
            cnt = 0;
            rst[cnt] = pi;
            div[cnt] = e;
            qud[cnt] = qd;
            rem[cnt] = r;

            do
            {
                cnt++;
                val = div[cnt - 1];
                ds = rem[cnt - 1];
                qd = val / ds;
                r = val % ds;

                if (r != 0)
                {
                    rst[cnt] = val;
                    div[cnt] = ds;
                    qud[cnt] = qd;
                    rem[cnt] = r;
                }
            } while (r != 0);
            long p1, q1, s1, t1, p2, q2, s2, t;

            p1 = rst[cnt - 1];
            q1 = -qud[cnt - 1];
            s1 = div[cnt - 1];
            t = 1;

            for (i = (cnt - 2); i >= 0; i--)
            {
                p2 = rst[i];
                q2 = -qud[i];
                s2 = div[i];
                if (s1 == rem[i])
                {
                    if (p1 == s2)
                    {
                        p1 = p2;
                        t1 = t;
                        t = q1;
                        q1 = t1 + (q1 * q2);
                        s1 = s2;
                    }
                }
            }
            if (q1 < 0)
                d = pi + q1;
            else
                d = q1;
            fe = e.ToString();
            fd = d.ToString();
            fn = n.ToString();

            if (fd.Length <= 8)
            {
                if (fd.Length == 7)
                {
                    fd = fd + "1";
                }
                else if (fd.Length == 6)
                {
                    fd = fd + "13";
                }
                else if (fd.Length == 5)
                {
                    fd = fd + "847";
                }
                else if (fd.Length == 4)
                {
                    fd = fd + "1437";
                }
                else if (fd.Length == 3)
                {
                    fd = fd + "13579";
                }
                else if (fd.Length == 2)
                {
                    fd = fd + "135790";
                }
            }
            else
            {
                fd = getSessionKey();
            }
            return fd;
        }


        private char userFlag(string kkey)
        {
            string kt = Convert.ToString(Convert.ToInt32(kkey, 10), 2);
         
            char kta = Convert.ToChar(kt.Substring((kt.Length) - 1));

            return kta;
        }
        public string key()
        {
            Randomkey randomkey = new Randomkey();
            UpdateKey ukey = new UpdateKey();
            var key = getSessionKey();
             

            var qkey = ukey.getQubit(randomkey.rand(), userFlag(Convert.ToInt32("1001101", 2).ToString()), key);
            if (qkey < 0)
            {
                qkey = -1 * qkey;
            }
          
            return qkey.ToString();
        }
    }
}
