using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumEncryption.Models
{
    public class UserData
    {
        public int UserDataId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string SenderPublicKey { get; set; }
        public string EncryptedData { get; set; }
        public DateTime SendingDatetime { get; set; }
    }
}
