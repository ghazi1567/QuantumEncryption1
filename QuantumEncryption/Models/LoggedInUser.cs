using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumEncryption.Models
{
    public class LoggedInUser
    {
        public int LoggedInUserId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserKey { get; set; }
        public string SessionId { get; set; }
        public string UserPrivateKey { get; set; }
        public string UserPublicKey { get; set; }
    }
}
