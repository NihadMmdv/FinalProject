﻿using Azure;
using Microsoft.AspNetCore.Mvc;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Subscribes;
using FinalProject.Service.Services.Interfaces;
using FinalProject.Service.ViewModels;
using Newtonsoft.Json;
using System.Speech.Synthesis;
using System.Text;

namespace FinalProject.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IBlogService _blogService;
        private readonly ITextWhyService _textWhyService;
        private readonly ISettingService _settingService;
        private readonly IAssociateService _associateService;
        private readonly ISubscribeService _subscribeService;
        private readonly ICarService _carService;
        private readonly IAccountService _accountService;
        public HomeController(ISliderService sliderService, IBlogService blogService, ITextWhyService textWhyService, ISettingService settingService, IAssociateService associateService, ISubscribeService subscribeService, ICarService carService, IAccountService accountService)
        {
            _sliderService = sliderService;
            _blogService = blogService;
            _textWhyService = textWhyService;
            _settingService = settingService;
            _associateService = associateService;
            _subscribeService = subscribeService;
            _carService = carService;
            _accountService = accountService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.IsDataLoading = true;
            var resultSlide = await _sliderService.GetAllAsync(0, 0);
            var resultBlog = await _blogService.GetAllAsync(0, 0, null);
            var resultText = await _textWhyService.GetAllAsync(0, 0);
            var resultAssociate = await _associateService.GetAllAsync(0, 0);
            var resultCar = await _carService.GetAllAsync(0, 0,x=>x.StatusId==1 || x.StatusId==2 && !x.IsDeleted);
            HomeVM homeVM = new HomeVM
            {
                Sliders = (IEnumerable<Slider>)resultSlide.items,
                Blogs = (IEnumerable<Blog>)resultBlog.items,
                Why = (IEnumerable<TextWhy>)resultText.items,
                Associates = (IEnumerable<Associate>)resultAssociate.items,
                Setting = _settingService.GetSetting().Result.Setting,
                Cars = (IEnumerable<Car>)resultCar.items,
            };
            ViewBag.IsDataLoading = false;
            return View(homeVM);
        }
        [HttpPost]
        public async Task<IActionResult> PostSubscribe(SubscribePostDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Mail"] = "Please add valid email";
                return RedirectToAction(nameof(Index));
            }
            var result= await _subscribeService.CreateAsync(dto);
            if(result.StatusCode != 201)
            {
                TempData["Mail"] = "This email is registered";
                return RedirectToAction(nameof(Index));
            }
            TempData["Verify"] = "Added succesfully";
            return RedirectToAction(nameof(Index));
        }

        private static HttpClient Http = new HttpClient();

        public async Task<IActionResult> SendMessageBot(string message)
        {
            AppUser user = (AppUser)(await _accountService.GetUser()).items;
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();

            if (user is not null)
            {
                if (user.UserPricingId == 1)
                {
                    string response = "Only Premium and Super users can use this Bot";
                    synthesizer.Speak(response);
                    return Json(response);
                }
                else
                {
                    Http = new HttpClient();
                    string response = null;

                    var apiKey = "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";
                    Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    var jsonContent = new
                    {
                        model = "gpt-3.5-turbo",
                        messages = new[]
                        {
                    new { role = "system", content = "You are a helpful assistant." },
                    new { role = "user", content = message }
                },
                        max_tokens = 1000
                    };

                    var responseContent = await Http.PostAsync("https://api.openai.com/v1/chat/completions",
                        new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json"));

                    var resContext = await responseContent.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<dynamic>(resContext);
                    response = data.choices[0].message.content.ToString();

                    return Json(response);
                }
            }
            else
            {
                string response = "Please Register for using Bot.";
                synthesizer.Speak(response);
                return Json(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            return View("Index");
        }
    }
}