using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quantum.API.Models
{
    public class TempTable
    {
        public int TempTableId { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int Table { get; set; }
        public int Index { get; set; }
        public int value { get; set; }
    }
}
