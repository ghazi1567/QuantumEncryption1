using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using QuantumEncryption.Helper;

namespace QuantumEncryption.Pages
{
    public class ConnectModel : PageModel
    {
        public void OnGet()
        {

        }
        public JsonResult OnGetMatrix()
        {
            //return new JsonResult(getMatrix(45,315));
            // return new JsonResult(getRandomMatrix());
            return new JsonResult(MatrixMultiply());
        }

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
            Int64 rnd = 100;
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

        static double[,] MatrixMultiply()
        {
            var a = getMatrix(45,315);
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
    }
}