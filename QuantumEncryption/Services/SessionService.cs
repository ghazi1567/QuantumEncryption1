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
            
            return new SessionModel
            {
                Id =user == null ?0 : user.LoggedInUserId,
                UserKey = user?.UserKey,
                UserName = user?.UserName,
            };
        }

        public List<LoggedInUser> GetLoggedInUsers(int sessionId)
        {
           return _context.LoggedInUsers.Where(x => x.LoggedInUserId != sessionId).ToList();
        }

        public LoggedInUser LoggedInUser(LoggedInUser user)
        {
            var arr = user.UserKey.Split(',');
            var determinant=  Matrix.GetDeterminant(double.Parse(arr[0]), double.Parse(arr[1]));
            Key key = KeyGenerator.GetKey(determinant);
            user.UserPrivateKey = key.PrivateKey;
            user.UserPublicKey = key.PublicKey;
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
