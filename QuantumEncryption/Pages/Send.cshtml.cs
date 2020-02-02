using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuantumEncryption.Helper;

namespace QuantumEncryption.Pages
{
    public class SendModel : PageModel
    {

        public SessionModel SessionModel { get; set; }
        public void OnGet()
        {
            SessionModel = Session.GetSessionModel(HttpContext.Session);
        }
        [BindProperty]
        public IFormFile File { get; set; }
        [BindProperty]
        public string EncryptedText { get; set; }
        public async Task<FileResult> OnPostEncryptAsync()
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
           
            return File(Encoding.UTF8.GetBytes(RSA.StartEncryption(fileContents, 0, 0)), "text/plain", "EncryptedText.txt");
        }
        public async Task<FileResult> OnPostDecryptAsync()
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
          
            return File(Encoding.UTF8.GetBytes(RSA.StartDecryption(fileContents)), "text/plain", "DecryptedText.txt");
        }
    }
}