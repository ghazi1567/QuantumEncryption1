using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuantumEncryption.Helper;

namespace QuantumEncryption.Pages
{
    public class IndexModel : PageModel
    {
      
        public SessionModel SessionModel { get; set; }
        public void OnGet()
        {
            SessionModel = Session.GetSessionModel(HttpContext.Session);
        }
    }
}
