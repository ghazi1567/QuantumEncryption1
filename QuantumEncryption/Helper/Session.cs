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
                UserKey = session.Get("loggedUserKey")?.ToString(),
                UserName = session.Get("loggedUser")?.ToString(),
            };
        }
    }
    public class SessionModel
    {
        public string UserName { get; set; }
        public string UserKey { get; set; }
    }
}
