using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quantum.API.Helper;

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
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            var aa  = RSA.Encryption(id, false);
            var bb = RSA.Decryption(aa, false);
            return bb;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
