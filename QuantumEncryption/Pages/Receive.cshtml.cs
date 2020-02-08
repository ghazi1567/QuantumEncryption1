using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    public class ReceiveModel : PageModel
    {
        private readonly ISessionService _service;
        private readonly AppDbContext _appDbContext;
        public ReceiveModel(ISessionService context, AppDbContext appDbContext)
        {
            _service = context;
            _appDbContext = appDbContext;
        }
        public List<LoggedInUser> LoggedInUsers { get; set; }
        public List<UserData> UserDatas { get; set; }
        public SessionModel SessionModel { get; set; }
        [BindProperty]
        public IFormFile File { get; set; }

        public ActionResult OnGet(int Id)
        {
            SessionModel = _service.GetCurrentUser(Id);
            if (string.IsNullOrEmpty(SessionModel.UserKey) || string.IsNullOrEmpty(SessionModel.UserName))
            {
                return RedirectToPage("Login");
            }
            UserDatas = _appDbContext.UserDatas.Where(x => x.ReceiverName == SessionModel.UserName).ToList();

            return Page();
        }

        public async Task<FileResult> OnPostDecryptAsync(int Id)
        {
            var filePath = Path.GetTempFileName();
            //var result ="";
            string fileContents;
            using (var stream = System.IO.File.Create(filePath))
            {
                await File.CopyToAsync(stream);
            }
            using (StreamReader reader = new StreamReader(filePath))
            {
                fileContents = reader.ReadToEnd();
            }
            var user = await _appDbContext.LoggedInUsers.FindAsync(Id);
            return File(Encoding.UTF8.GetBytes(RSA.StartDecryption(fileContents,user.UserPrivateKey)), "text/plain", "DecryptedText.txt");
        }
        public ActionResult OnGetDownload(int Id)
        {
            
            var data = _appDbContext.UserDatas.Find(Id);
            return File(Encoding.UTF8.GetBytes(data.EncryptedData), "text/plain", "EcryptedText.txt");
        }
    }
}