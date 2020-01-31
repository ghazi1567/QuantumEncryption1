using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quantum.API.Models
{
    public class file_request
    {
        public string key { get; set; }
        public IFormFile file { get; set; }
    }
}
