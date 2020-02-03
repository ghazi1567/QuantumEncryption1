﻿using System;
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

namespace QuantumEncryption.Pages
{
    public class SendModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public SendModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<UserData> UserDatas { get; set; }
        public SessionModel SessionModel { get; set; }
        public ActionResult OnGet()
        {
            SessionModel = Session.GetSessionModel(HttpContext.Session);
            //if (string.IsNullOrEmpty(SessionModel.UserKey) || string.IsNullOrEmpty(SessionModel.UserName))
            //{
            //    return RedirectToPage("Login");
            //}
            UserDatas = _appDbContext.UserDatas.Where(x=>x.SenderName == SessionModel.UserName).ToList();
            return Page();
        }
        [BindProperty]
        public IFormFile File { get; set; }
        [BindProperty]
        public string ReceiverName { get; set; }
        public async Task<FileResult> OnPostEncryptAsync()
        {
            #region ReadFileContent
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
            #endregion

            // Encryotion
            var encryptesData = RSA.StartEncryption(fileContents,SessionModel.UserKey);

            #region Sending data to Receiver
            var userdata = new UserData
            {
                SenderName = SessionModel?.UserName,
                ReceiverName = ReceiverName,
                SenderPublicKey = $"{RSA.d},{RSA.n}",
                EncryptedData = encryptesData
            };
            _appDbContext.UserDatas.Add(userdata);
            _appDbContext.SaveChanges();
            #endregion


            return File(Encoding.UTF8.GetBytes(encryptesData), "text/plain", "EncryptedText.txt");
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