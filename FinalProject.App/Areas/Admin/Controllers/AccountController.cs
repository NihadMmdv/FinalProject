using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Data;
using FinalProject.Service.Dtos.Accounts;
using FinalProject.Service.Services.Implementations;
using FinalProject.Service.Services.Interfaces;
using FinalProject.Service.ViewModels;

namespace FinalProject.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly IEmailService _mailService;
        private readonly ICountryService _countryService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IEmailService mailService, IAccountService service, ICountryService countryService, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _mailService = mailService;
            _service = service;
            _countryService = countryService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Authorize]
        public async Task<IActionResult> Info()
        {
            var result = await _service.Info();
            if (result.StatusCode != 200)
            {
                TempData["AdminInfo"] = result.Description;
                return RedirectToAction("index", "home");
            }
            InfoVM info = (InfoVM)result.items;
            return View(info);
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var result = await _service.Login(dto, false);
            if (result.StatusCode != 200)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            return RedirectToAction("index", "home");
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            var result = await _service.LogOut();
            return RedirectToAction("index", "home");
        }
        [Authorize]
        public async Task<IActionResult> Update()
        {
            var resultCountry = await _countryService.GetAllAsync(0, 0);
            ViewBag.Countries = resultCountry.items;
            var result = await _service.GetUser();
            if (result.StatusCode != 203)
            {
                return NotFound();
            }
            AppUser user = (AppUser)result.items;
            UpdateDto dto = new UpdateDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                CountryId = user.CountryId,
                Image = user.Image
            };
            return View(dto);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(UpdateDto dto)
        {
            var resultCountry = await _countryService.GetAllAsync(0, 0);
            ViewBag.Countries = resultCountry.items;
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var result = await _service.Update(dto, null);
            if (result.StatusCode != 203)
            {
                if (result.StatusCode == 404)
                {
                    return NotFound();
                }
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            return RedirectToAction(nameof(Info));
        }
        /*
        public async Task<IActionResult> CreateSuperAdminUser()
        {
            var userExists = await _userManager.FindByEmailAsync("superadmin@TurboBid.com");
            if (userExists == null)
            {
                AppUser superadmin = new AppUser
                {
                    UserName = "SuperAdmin",
                    Email = "superadmin@TurboBid.com",
                    Name = "SuperAdmin",
                    Surname = "SuperAdmin",
                    EmailConfirmed = true,
                    CountryId = 1,
                    UserPricingId = 3,
                    Image = "nihad.jpg",
                };

                var result = await _userManager.CreateAsync(superadmin, "Superadmin123!");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(superadmin, "SuperAdmin");
                    return Json("SuperAdmin user created and assigned to SuperAdmin role.");
                }
                else
                {
                    return Json(result.Errors);
                }
            }
            return Json("User already exists.");
        }

        
        public async Task<IActionResult> CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }

            return Json("Roles Created Successfully");
        }
        */
    }
}
