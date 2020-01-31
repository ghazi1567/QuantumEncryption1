using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quantum.API.Helper;
using Quantum.API.Models;

namespace Quantum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        public static List<int> baseList { get; set; } = new List<int>() { 0,45,90,135};
        static Random rnd = new Random();
        // GET api/values
        [HttpGet]
        public ActionResult<int> Get()
        {
            int r = rnd.Next(baseList.Count);
            return baseList[r];
        }

        // GET api/values/5
        [HttpGet("{id}/{key}")]
        public ActionResult<string> Get(string id, string key)
        {
            //var aa  = RSA.Encryption(id, key);
            //var bb = RSA.Decryption(aa, key);
            return "";
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
            //var result = RSA.Encryption(Encoding.UTF8.GetBytes(fileContents), file.key);
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
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
