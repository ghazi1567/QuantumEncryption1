using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuantumEncryption.Helper;

namespace QuantumEncryption.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {

        public string UserName { get; set; }
        public string UserId { get; set; }
        public string UserKey { get; set; }
        
        public SessionModel SessionModel { get; set; }

        public void OnGet()
        {
            SessionModel = Session.GetSessionModel(HttpContext.Session);
        }
        public ActionResult OnPost()
        {
            HttpContext.Session.SetString("loggedUserId", UserId);
            HttpContext.Session.SetString("loggedUser", UserName);
            HttpContext.Session.SetString("loggedUserKey", UserKey);
            return RedirectToPage("Send");
        }

        
    }

}