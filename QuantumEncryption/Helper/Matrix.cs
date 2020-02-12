using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumEncryption.Helper
{
    public static class Matrix
    {
        static double getAngle(double degrees)
        {
            double angle = Math.PI * degrees / 180.0;
            return angle;
        }
        static double[,] getMatrix(double sinDegre, double cosDegree)
        {
            int rnd = 4;
            double[,] myArray = new double[4, 4];
            myArray[0, 0] = 1; myArray[0, 1] = 0; myArray[0, 2] = Math.Round(Math.Cos(getAngle(cosDegree)), rnd); myArray[0, 3] = Math.Round(Math.Sin(getAngle(sinDegre)), rnd);
            myArray[1, 0] = 0; myArray[1, 1] = 1; myArray[1, 2] = Math.Round(Math.Sin(getAngle(sinDegre)), rnd); myArray[1, 3] = Math.Round(Math.Cos(getAngle(cosDegree)), rnd);
            myArray[2, 0] = Math.Round(Math.Cos(getAngle(cosDegree)), rnd); myArray[2, 1] = Math.Round(Math.Sin(getAngle(sinDegre)), rnd); myArray[2, 2] = Math.Round(Math.Exp(sinDegre), 1); myArray[2, 3] = 0;
            myArray[3, 0] = Math.Round(Math.Sin(getAngle(sinDegre)), rnd); myArray[3, 1] = Math.Round(Math.Cos(getAngle(cosDegree)), rnd); myArray[3, 2] = 0; myArray[3, 3] = Math.Exp(-sinDegre);

            return myArray;
        }
        static double[,] getRandomMatrix()
        {
            Int64 rnd = 11000;
            double[,] myArray = new double[4, 4];
            myArray[0, 0] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[0, 1] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[0, 2] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[0, 3] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;

            myArray[1, 0] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[1, 1] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[1, 2] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[1, 3] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;

            myArray[2, 0] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[2, 1] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[2, 2] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[2, 3] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;

            myArray[3, 0] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[3, 1] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[3, 2] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;
            myArray[3, 3] = RSA.GetPrimeNumber(rnd); rnd = rnd - 5;

            return myArray;
        }
        static double[,] MatrixMultiply(double sinDegre, double cosDegree)
        {
            var a = getMatrix(sinDegre,cosDegree);
            var b = getRandomMatrix();
            int m = 4, n = 4, p = 4, q = 4, i, j;
            double[,] c = new double[m, q];
            for (i = 0; i < m; i++)
            {
                for (j = 0; j < q; j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        c[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return c;
        }
        //this method determines the value of determinant using recursion
        static double Determinant(double[,] input)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            if (order > 2)
            {
                double value = 0;
                for (int j = 0; j < order; j++)
                {
                    double[,] Temp = CreateSmallerMatrix(input, 0, j);
                    value = Math.Round(value, 2) + Math.Round(input[0, j], 2) * (SignOfElement(0, j) * Determinant(Temp));
                }
                return Math.Round(value, 2);
            }
            else if (order == 2)
            {
                return Math.Round((Math.Round(input[0, 0], 2) * Math.Round(input[1, 1], 2)) - (Math.Round(input[1, 0], 2) * Math.Round(input[0, 1], 2)), 2);
            }
            else
            {
                return Math.Round(input[0, 0], 2);
            }
        }
        //this method determines the sub matrix corresponding to a given element
        static double[,] CreateSmallerMatrix(double[,] input, int i, int j)
        {
            int order = int.Parse(System.Math.Sqrt(input.Length).ToString());
            double[,] output = new double[order - 1, order - 1];
            int x = 0, y = 0;
            for (int m = 0; m < order; m++, x++)
            {
                if (m != i)
                {
                    y = 0;
                    for (int n = 0; n < order; n++)
                    {
                        if (n != j)
                        {
                            output[x, y] = input[m, n];
                            y++;
                        }
                    }
                }
                else
                {
                    x--;
                }
            }
            return output;
        }
        //this method determines the sign of the elements
        static int SignOfElement(int i, int j)
        {
            if ((i + j) % 2 == 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }


       public  static Int64 GetDeterminant(double sinDegre, double cosDegree)
        {  // return new JsonResult(getRandomMatrix());
            var _val = Determinant(MatrixMultiply(sinDegre, cosDegree));

            string decimal_places = "";
            var regex = new System.Text.RegularExpressions.Regex("(?<=[\\.])[0-9]+");
            if (regex.IsMatch(_val.ToString()))
            {
                decimal_places = regex.Match(_val.ToString()).Value;
            }
            var bigNumber = Int64.Parse(decimal_places);
            return bigNumber;
        }
    }
}
