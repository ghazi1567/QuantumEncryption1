using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quantum.API.Contexts;
using Quantum.API.Helper;
using Quantum.API.Models;

namespace Quantum.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CommonController(AppDbContext context)
        {
            _context = context;
        }
        public static List<int> baseList { get; set; } = new List<int>() { 0,45,90,135};
        static Random rnd = new Random();
        // GET api/values
        [HttpGet("Common")]
        public ActionResult<int> Get()
        {
            int r = rnd.Next(baseList.Count);
            return baseList[r];
        }

        [HttpGet("key")]
        public ActionResult<string> Getkey()
        {
            var ss = new SessionKey(173, 211);
            var key = ss.key();

            return key;
        }
        // POST api/values
        [HttpPost]
        public async Task<FileResult> PostAsync([FromForm]file_request file)
        {
            var filePath = Path.GetTempFileName();
            //var result ="";
           string fileContents;
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.file.CopyToAsync(stream);
                
            }
            using (StreamReader reader = new StreamReader(filePath))
            {
                fileContents = reader.ReadToEnd();
            }
            var result1 = RSA.Encryption_old(fileContents, file.key);
            var result = RSA.EncryptionStr(Encoding.UTF8.GetBytes(fileContents), file.key);
            return File(Encoding.UTF8.GetBytes(result), "text/plain", "foo.txt");

            //return File(Encoding.UTF8.GetBytes(result), "text/plain", "foo.txt");

            //return result;
        }

        [HttpPost("Decryption")]
        public async Task<ActionResult<string>> PostDecryptAsync([FromForm]file_request file)
        {
            var filePath = Path.GetTempFileName();
            
            using (var stream = System.IO.File.Create(filePath))
            {
                await file.file.CopyToAsync(stream);
              
            }
            
            string fileContents;
            using (StreamReader reader = new StreamReader(filePath))
            {
                fileContents = reader.ReadToEnd();
            }


            var result = RSA.Decryptionstr(Encoding.UTF8.GetBytes(fileContents), file.key);
            return File(Encoding.UTF8.GetBytes(result), "text/plain", "foo_dec.txt");
        }
        //Connect/{SenderId}/{ReceiverId}/{Table}/{Index}
        [HttpGet("Connect/{s}/{r}/{t}/{i}")]
        public ActionResult<TempTable> GetConnect(int s,int r,int t,int i)
        {
            var get = GetTableIndex(s,r,t,i);
            if (get == null)
            {
                GenerateTable(s, r, t);
                get = GetTableIndex(s, r, t, i);
            }

            return get;
        }


        private TempTable GetTableIndex(int s, int r, int t, int i)
        {
          return  _context.TempTables.SingleOrDefault(x => x.SenderId == s && x.ReceiverId == r && x.Table == t && x.Index == i);
        }


        private List<TempTable> GenerateTable(int s, int r, int t)
        {
            var rBases =  _context.UserBases.Where(x => x.UserId == r).ToArray();

            int length = rBases .Length > t*10 ? rBases.Length : t * 10;

            List<TempTable> temps = new List<TempTable>();
            temps.Add(new TempTable
            {
                SenderId = s,
                ReceiverId = r,
                Table = t,
                Index = 0,
                value = rBases.Length
            });


            int i = 1;
            for (int j = 1; j <= length; j++)
            {
                if (j == t * i)
                {
                    if (rBases.Length >= i)
                    {
                        temps.Add(new TempTable
                        {
                            SenderId = s,
                            ReceiverId = r,
                            Table = t,
                            Index = j,
                            value = rBases[i - 1].Degree
                        });
                        i++;
                    }
                }
                else
                {
                    temps.Add(new TempTable
                    {
                        SenderId = s,
                        ReceiverId = r,
                        Table = t,
                        Index = j,
                        value = baseList[rnd.Next(baseList.Count)]
                    });
                }
            }

            var existing = _context.TempTables.Where(x => x.SenderId == s && x.ReceiverId == r).ToList();
            _context.TempTables.RemoveRange(existing);
            _context.SaveChanges();
            _context.TempTables.AddRange(temps);
            _context.SaveChanges();
            return temps;
        }

    }
}
