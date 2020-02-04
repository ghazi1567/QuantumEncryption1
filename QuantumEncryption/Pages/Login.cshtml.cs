using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuantumEncryption.Contexts;
using QuantumEncryption.Helper;
using QuantumEncryption.Models;
using QuantumEncryption.Services;

namespace QuantumEncryption.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly ISessionService _service;
        public LoginModel(ISessionService context)
        {
            _service = context;
        }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserKey { get; set; }
        
        public SessionModel SessionModel { get; set; }

        public void OnGet(int Id = 0)
        {
            SessionModel = _service.GetCurrentUser(Id);
            _service.LogoutCurrentUser(Id);
        }
        public ActionResult OnPost()
        {
            var user = new LoggedInUser
            {
                 UserId=UserId,
                 UserKey=UserKey,
                 UserName=UserName,
                 SessionId = HttpContext.Session.Id
            };
            var result = _service.LoggedInUser(user);
            return RedirectToPage("Send",new { id = result.LoggedInUserId });
        }
        public ActionResult OnGetLogout(int Id)
        {
            _service.LogoutCurrentUser(Id);
            return RedirectToPage("Login");
        }

    }

}