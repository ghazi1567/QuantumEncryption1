using QuantumEncryption.Helper;
using QuantumEncryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumEncryption.Services
{
    public interface ISessionService
    {
        LoggedInUser LoggedInUser(LoggedInUser user);
        SessionModel GetCurrentUser(int sessionId);
        void LogoutCurrentUser(int sessionId);

        List<LoggedInUser> GetLoggedInUsers(int sessionId);
        string RequestPublicKey(int UserId);
    }
}
