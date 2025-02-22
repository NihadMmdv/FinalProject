using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Service.Services.Interfaces;
using System.Security.Claims;
using FinalProject.DAL.Entities;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using FinalProject.Service.Dtos.Cars;

namespace FinalProject.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CarController : Controller
    {
        private readonly ICarService _service;
        private readonly IFuelService _fuelService;
        private readonly IBanService _banService;
        private readonly IColorService _colorService;
        private readonly ICountryService _countryService;
        private readonly IBrandService _brandService;
        private readonly IAccountService _accountService;
        private readonly ICarImageService _carImageService;
        private readonly ILogger<CarController> _logger;

        public CarController(ICarService service, IFuelService fuelService, IBanService banService,
                             IColorService colorService, ICountryService countryService, IBrandService brandService,
                             IAccountService accountService, ICarImageService carImageService, ILogger<CarController> logger)
        {
            _service = service;
            _fuelService = fuelService;
            _banService = banService;
            _colorService = colorService;
            _countryService = countryService;
            _brandService = brandService;
            _accountService = accountService;
            _carImageService = carImageService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllAsync(0, 0, x => !x.IsDeleted);
            var models = (result.items as IEnumerable<Car>)?.ToList() ?? new List<Car>();

            int totalCount = models.Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)totalCount / 5);
            ViewBag.CurrentPage = page;

            result = await _service.GetAllAsync(5, page, null);
            return View(result.items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadViewBags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarPostDto dto)
        {
            await LoadViewBags();

            if (!ModelState.IsValid)
                return View(dto);

            var result = await _service.CreateAsync(dto);
            if (result.StatusCode == 404)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }

            _logger.LogInformation("Car Created by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            await LoadViewBags(id);
            var result = await _service.GetAsync(id);

            if (result.StatusCode == 404)
                return NotFound();

            return View(result.items);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CarUpdateDto dto)
        {
            await LoadViewBags(id);

            if (!ModelState.IsValid)
                return View(dto);

            var resultImage = await _carImageService.GetAllAsync(0, 0, x => x.CarId == id && !x.IsDeleted);
            var images = (resultImage.items as IEnumerable<CarImage>)?.ToList() ?? new List<CarImage>();

            if (dto.FormFiles is not null)
            {
                int length = images.Count + dto.FormFiles.Count();
                if (length > 10 || length < 3)
                {
                    ModelState.AddModelError("FormFiles", "Min 3, Max 10 Images");
                    return View(dto);
                }
            }

            var result = await _service.UpdateAsync(id, dto);
            if (result.StatusCode == 400)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }

            _logger.LogInformation("Car Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            var result = await _service.RemoveAsync(id);
            if (result.StatusCode == 404)
                return NotFound();

            _logger.LogInformation("Car Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SetAsMainImage(int id)
        {
            var result = await _carImageService.GetAsync(id, null);
            var carImage = result.itemView as CarImage;

            if (carImage == null)
                return Json(new { status = 404 });

            carImage.isMain = true;

            result = await _carImageService.GetAsync(id, x => x.isMain && x.CarId == carImage.CarId);
            var existingMainImage = result.itemView as CarImage;

            if (existingMainImage is not null)
                existingMainImage.isMain = false;

            await _carImageService.Save();
            _logger.LogInformation("Car Main Image Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Json(new { status = 200 });
        }

        public async Task<IActionResult> RemoveImage(int id)
        {
            var result = await _carImageService.GetAllAsync(0, 0, x => !x.IsDeleted && x.Id == id);
            var carImage = (result.items as IEnumerable<CarImage>)?.FirstOrDefault();

            if (carImage == null)
                return Json(new { status = 404, desc = "Image not found" });

            if (carImage.isMain)
                return Json(new { status = 400, desc = "You cannot remove the main image" });

            carImage.IsDeleted = true;
            await _carImageService.Save();

            _logger.LogInformation("Car Image Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Json(new { status = 200 });
        }

        /// <summary>
        /// Loads ViewBags with necessary data to prevent code duplication
        /// </summary>
        private async Task LoadViewBags(int? carId = null)
        {
            ViewBag.Users = (await _accountService.GetAllUsers(0, 0)).items;
            ViewBag.Fuels = (await _fuelService.GetAllAsync(0, 0)).items;
            ViewBag.Bans = (await _banService.GetAllAsync(0, 0)).items;
            ViewBag.Colors = (await _colorService.GetAllAsync(0, 0)).items;
            ViewBag.Countries = (await _countryService.GetAllAsync(0, 0)).items;
            ViewBag.Brands = (await _brandService.GetAllAsync(0, 0)).items;

            if (carId.HasValue)
                ViewBag.Images = (await _carImageService.GetAllAsync(0, 0, x => x.CarId == carId.Value && !x.IsDeleted)).items;
        }
    }
}
