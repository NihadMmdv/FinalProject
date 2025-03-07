﻿using Microsoft.AspNetCore.Mvc;
using FinalProject.Service.ViewModels;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Messages;
using FinalProject.Service.Services.Interfaces;
using System.Text.RegularExpressions;

namespace FinalProject.App.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMessageService _service;
        private readonly ISettingService _settingService;
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;

        public ContactController(IMessageService service, ISettingService settingService, IEmailService emailService, IAccountService accountService)
        {
            _service = service;
            _settingService = settingService;
            _emailService = emailService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.IsDataLoading = true;
            var result =await _settingService.GetSetting();
            ContactVM contactVM = new ContactVM()
            {
                Setting = result.Setting,
            };
            ViewBag.IsDataLoading = false;
            return View(contactVM);
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(MessagePostDto dto)
        {
            var result = await _accountService.GetUserById(dto.appUserId);
            AppUser appUser = (AppUser) result.items;
            if(dto.Address is null)
            {
                dto.Address = "For dealer";
            }
            if (!ModelState.IsValid)
            {
                TempData["Email"] = "Please fill All fields";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            string strRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            Regex re = new Regex(strRegex);
            if (!re.IsMatch(dto.Email))
            {
                TempData["Email"] = "Please add valid email";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            if (!ModelState.IsValid)
            {
               TempData["Message"] = "Please fill all inputs";
                return Redirect(Request.Headers["Referer"].ToString());
            }
            result = await _service.CreateAsync(dto);
            TempData["Success"] = "Successfully send message";
            if (dto.Address == "For dealer")
            {
                string text = "User: " + dto.FirstName + "\n" +
                              "Email:" + dto.Email + "\n" +
                              "Number: " + dto.Phone + "\n" +
                              "Text: " + dto.Text + "\n";
                await _emailService.SendMail("turbobidofficial@gmail.com", appUser.Email,
                "Auction Info",text, null, appUser.Name + " " + appUser.Surname);
            }
            return Redirect(Request.Headers["Referer"].ToString());

        }
    }
}