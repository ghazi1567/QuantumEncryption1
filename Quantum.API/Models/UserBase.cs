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
        public int Sin { get; set; }
        public int Cos { get; set; }

        public int UserId { get; set; }
    }
}
