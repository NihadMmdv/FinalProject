using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Models;
using FinalProject.Service.Services.Interfaces;
using System.Security.Claims;

namespace FinalProject.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ModelController : Controller
    {
        private readonly IModelService _service;
        private readonly IBrandService _brandService;
        private readonly ILogger<ModelController> _logger;
        public ModelController(IModelService service, IBrandService brandService, ILogger<ModelController> logger)
        {
            _service = service;
            _brandService = brandService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                int itemsPerPage = 8;

                // Fetch all items only to get total count
                var allModelsResult = await _service.GetAllAsync(0, 0);
                if (allModelsResult.StatusCode != 200 || allModelsResult.items == null)
                {
                    ModelState.AddModelError("", "Failed to retrieve models.");
                    return View(new List<Model>()); // Return an empty list to avoid null exceptions
                }

                // Get total count safely
                var allModels = allModelsResult.items as IEnumerable<Model> ?? new List<Model>();
                int totalCount = allModels.Count();

                ViewBag.TotalPage = (int)Math.Ceiling((decimal)totalCount / itemsPerPage);
                ViewBag.CurrentPage = page;

                // Fetch only paginated data
                var paginatedResult = await _service.GetAllAsync(itemsPerPage, page);
                if (paginatedResult.StatusCode != 200 || paginatedResult.items == null)
                {
                    ModelState.AddModelError("", "Failed to retrieve paginated models.");
                    return View(new List<Model>());
                }

                return View(paginatedResult.items);
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                _logger.LogError(ex, "An error occurred while fetching models for the index page.");

                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
                return View(new List<Model>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = await _brandService.GetAllAsync(0, 0);
			ViewBag.Brands = result.items;
			return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModelPostDto dto)
        {
            var result = await _brandService.GetAllAsync(0, 0);
            ViewBag.Brands = result.items;

            if (!ModelState.IsValid)
            {
                return View(dto);
            }
             result = await _service.CreateAsync(dto);
            if (result.StatusCode == 400)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            _logger.LogInformation("Model Created by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _brandService.GetAllAsync(0, 0);
            ViewBag.Brands = result.items;

            result = await _service.GetAsync(id);
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            return View(result.items);
        }
        public async Task<IActionResult> Update(int id,ModelUpdateDto dto)
        {
            var result = await _brandService.GetAllAsync(0, 0);
            ViewBag.Brands = result.items;

            if (!ModelState.IsValid)
            {
                return View(dto);
            }
             result = await _service.UpdateAsync(id,dto);
            if (result.StatusCode == 400)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            _logger.LogInformation("Model Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _service.RemoveAsync(id);
            if(result.StatusCode == 404)
            {
                return NotFound();
            }
            _logger.LogInformation("Model Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
    }
}
