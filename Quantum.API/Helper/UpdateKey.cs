using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quantum.API.Helper
{
    public class UpdateKey
    {

        static string qqb;
        static public int zCount;
        static public int oCount;

        public UpdateKey()
        {
            // getQubit(rta, kta, Sesskey);
        }

        public int getQubit(char rta, char kta, string sesskey1)
        {
            string sesskey = sesskey1;
            string skb = Convert.ToString(Convert.ToInt32(sesskey, 10), 2);


            Console.WriteLine("SessKey Binary: " + skb);
            //Calling getZero & getOnes Function
            int countZero = getZeros(skb);
            int countOne = getOnes(skb);

            zCount = countZero;
            oCount = countOne;

            //Generate Quantum Key after applying Quantum formula
            int qk = genrateQK(rta, kta, countZero, countOne);
            return qk;
        }

        //Applying Quantum Key Formulae
        public int genrateQK(char rta, char kta, int countZero, int countOne)
        {
            int qbt = 0;
            if (rta == '0' && kta == '0')
            {
                qbt = (int)(0.707 * (countZero + countOne));
                qqb = "00";
            }
            else if (rta == '1' && kta == '0')
            {
                qbt = (int)(0.707 * (countZero - countOne));
                qqb = "10";
            }
            else if (rta == '0' && kta == '1')
            {
                qbt = countZero;
                qqb = "01";
            }
            else if (rta == '1' && kta == '1')
            {
                qbt = countOne;
                qqb = "11";
            }

            return qbt;
        }

        //Function for Counting Zeros
        public int getZeros(string sesskey)
        {
            int countZero = 0;
            for (int i = 0; i < sesskey.Length; i++)
            {
                char? c;
                c = Convert.ToChar(sesskey.Substring((i), 1));

                if (c == '0')
                {
                    countZero++;
                }
                c = null;
            }
            Console.WriteLine("|0> : " + countZero);
            return countZero;
        }

        //Function for Counting Ones 
        public int getOnes(string sesskey)
        {

            int countOne = 0;
            for (int i = 0; i < sesskey.Length; i++)
            {
                char c = Convert.ToChar(sesskey.Substring((i), 1));
                if (c == '1')
                {
                    countOne++;
                }
            }
            Console.WriteLine("|1> : " + countOne);
            return countOne;
        }

    }

    public class Randomkey
    {
        public char rand()
        {
            Random rnd = new Random();
            long a = rnd.Next(100000000);
            string rn = a.ToString();

            

            string rt = Convert.ToString(Convert.ToInt32(rn, 10), 2);
            char rta = Convert.ToChar(rt.Substring((rt.Length) - 1));
            return rta;
        }

    }
}
