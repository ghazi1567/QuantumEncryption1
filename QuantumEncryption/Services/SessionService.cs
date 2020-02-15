using QuantumEncryption.Contexts;
using QuantumEncryption.Helper;
using QuantumEncryption.Models;
using QuantumEncryption.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuantumEncryption
{
    public class SessionService: ISessionService
    {
        private readonly AppDbContext _context;
        public SessionService(AppDbContext context)
        {
            _context = context;
        }

        public SessionModel GetCurrentUser(int sessionId)
        {
            var user = _context.LoggedInUsers.Find(sessionId);
            if (user == null)
            {
                user = new LoggedInUser();
            }
            return new SessionModel
            {
                Id =user == null ?0 : user.LoggedInUserId,
                UserKey = user?.UserKey,
                UserName = user?.UserName,
                Decimal=user.Decimal,
                Binary=user.Binary,
                Q=user.Q,
                P=user.P,
                UserPrivateKey=user.UserPrivateKey,
                UserPublicKey=user.UserPublicKey
            };
        }

        public List<LoggedInUser> GetLoggedInUsers(int sessionId)
        {
           return _context.LoggedInUsers.Where(x => x.LoggedInUserId != sessionId).ToList();
        }

        public LoggedInUser LoggedInUser(LoggedInUser user)
        {
            int userKeyNumber = Convert.ToInt32(user.UserKey, 2);
            Key key = KeyGenerator.GetKey(userKeyNumber);
            user.UserPrivateKey = key.PrivateKey;
            user.UserPublicKey = key.PublicKey;
            user.P = key.P;
            user.Q = key.Q;
            user.Binary = user.UserKey.ToString();
            user.Decimal = userKeyNumber.ToString();

            _context.LoggedInUsers.Add(user);
            _context.SaveChanges();

            return user;
        }
        public void LogoutCurrentUser(int sessionId)
        {
            var user = _context.LoggedInUsers.Find( sessionId);
            if (user != null)
            {
                _context.LoggedInUsers.Remove(user);
                _context.SaveChanges(); 
            }
        }

        public string RequestPublicKey(int UserId)
        {
            var receiver = _context.LoggedInUsers.Find(UserId);
            return receiver.UserPublicKey;
        }
    }
}
