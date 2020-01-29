using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quantum.API.Models
{
    public class LoginRequest
    {
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string Password { get; set; }
    }
}
