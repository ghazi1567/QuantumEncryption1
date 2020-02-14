using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumEncryption.Helper
{
    public static class Session{
        public static SessionModel GetSessionModel(ISession session)
        {
            return new SessionModel
            {
                UserKey = session.GetString("loggedUserKey"),
                UserName = session.GetString("loggedUser"),
            };
        }
    }
    public class SessionModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserKey { get; set; }
        public string UserPrivateKey { get; set; }
        public string UserPublicKey { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string Binary { get; set; }
        public string Decimal { get; set; }
    }
}
