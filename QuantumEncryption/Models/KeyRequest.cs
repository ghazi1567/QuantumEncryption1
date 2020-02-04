using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumEncryption.Models
{
    public class KeyRequest
    {
        public int KeyRequestId { get; set; }
        public int SenderId { get; set; }
        public string Sender { get; set; }
        public int ReceiverId { get; set; }
        public string Receiver { get; set; }
        public string SenderPublicKey { get; set; }
        public string ReceiverPublicKey { get; set; }

    }

    public class Key
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
