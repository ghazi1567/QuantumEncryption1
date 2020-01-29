using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Quantum.API.Models
{
    public class UserBase
    {
        public int UserBaseId { get; set; }
        public int Degree { get; set; }
        public bool Bit { get; set; }

        public int UserId { get; set; }
    }
}
