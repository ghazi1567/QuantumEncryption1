using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuantumEncryption.Helper;
using QuantumEncryption.Services;

namespace QuantumEncryption.Pages
{
    public class RSAKeyModel : PageModel
    {
        private readonly ISessionService _service;
        public RSAKeyModel(ISessionService context)
        {
            _service = context;
        }
        public SessionModel SessionModel { get; set; }
        public void OnGet(int Id)
        {
            SessionModel = _service.GetCurrentUser(Id);
        }
    }
}