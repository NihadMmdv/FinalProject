﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Data;
using FinalProject.Service.Dtos.Categories;
using FinalProject.Service.Dtos.Colors;
using FinalProject.Service.Dtos.UserPricings;
using FinalProject.Service.Services.Interfaces;
using System.Security.Claims;

namespace FinalProject.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ColorController : Controller
    {
        private readonly IColorService _service;
        private readonly ILogger<ColorController> _logger;

        public ColorController( IColorService service, ILogger<ColorController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllAsync(0, 0);
            int TotalCount = ((IEnumerable<Color>)result.items).Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalCount / 8);
            ViewBag.CurrentPage = page;
            int count = 8;
            result = await _service.GetAllAsync(count, page);
            return View(result.items);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ColorPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var result = await _service.CreateAsync(dto);
            if (result.StatusCode == 400)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            _logger.LogInformation("Color Created by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _service.GetAsync(id);
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            return View(result.items);
        }
        public async Task<IActionResult> Update(int id, ColorUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var result = await _service.UpdateAsync(id, dto);
            if (result.StatusCode == 400)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            _logger.LogInformation("Color Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _service.RemoveAsync(id);
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            _logger.LogInformation("Color Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
    }
}
