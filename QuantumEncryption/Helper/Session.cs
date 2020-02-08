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
    }
}
