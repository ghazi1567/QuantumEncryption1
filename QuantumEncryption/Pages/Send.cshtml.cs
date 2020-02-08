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
using QuantumEncryption.Services;

namespace QuantumEncryption.Pages
{
    public class SendModel : PageModel
    {
        private readonly ISessionService _service;
        private readonly AppDbContext _appDbContext;
        public SendModel(ISessionService context, AppDbContext appDbContext)
        {
            _service = context;
            _appDbContext = appDbContext;
        }
        public List<LoggedInUser> LoggedInUsers { get; set; }
        public List<UserData> UserDatas { get; set; }
        public SessionModel SessionModel { get; set; }
        public ActionResult OnGet(int Id)
        {
            SessionModel = _service.GetCurrentUser(Id);
            if (string.IsNullOrEmpty(SessionModel.UserKey) || string.IsNullOrEmpty(SessionModel.UserName))
            {
                return RedirectToPage("Login");
            }
            SenderName = SessionModel.UserName;
            UserDatas = _appDbContext.UserDatas.Where(x=>x.SenderName == SessionModel.UserName).ToList();
            LoggedInUsers = _service.GetLoggedInUsers(Id);
            return Page();
        }
        [BindProperty]
        public IFormFile File { get; set; }
        [BindProperty]
        public string ReceiverName { get; set; }
        [BindProperty]
        public string ReceiverKey { get; set; }
        [BindProperty]
        public string SenderName { get; set; }
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
            var encryptesData = RSA.StartEncryption(fileContents,ReceiverKey);

            #region Sending data to Receiver
            var userdata = new UserData
            {
                SenderName = SenderName,
                ReceiverName = ReceiverName,
                SenderPublicKey = $"{RSA.d},{RSA.n}",
                EncryptedData = encryptesData,
                SendingDatetime=DateTime.Now
            };
            _appDbContext.UserDatas.Add(userdata);
            _appDbContext.SaveChanges();
            #endregion


            return File(Encoding.UTF8.GetBytes(encryptesData), "text/plain", "EncryptedText.txt");
        }
        
        public JsonResult OnGetConnect(int Id)
        {
            return new JsonResult(_service.RequestPublicKey(Id));
        }
    }
}