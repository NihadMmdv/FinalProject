﻿using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Accounts;
using FinalProject.Service.Dtos.Cars;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using FinalProject.Service.ViewModels;

namespace FinalProject.App.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IBraintreeService _braintreeService;
        private readonly IBookService _bookService;
        private readonly ICarService _carService;
        private readonly IEmailService _emailService;
        private readonly IAccountService _accountService;
        private readonly IUserPricingService _userPricingService;
        public PaymentController(IBraintreeService braintreeService, IBookService bookService, ICarService carService, IEmailService emailService, IAccountService accountService, IUserPricingService userPricingService)
        {
            _braintreeService = braintreeService;
            _bookService = bookService;
            _carService = carService;
            _emailService = emailService;
            _accountService = accountService;
            _userPricingService = userPricingService;
        }
        public async Task<IActionResult> Success()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> UpdateUser(int pricingId,string userId)
        {
            var gateway = _braintreeService.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            ViewBag.ClientToken = clientToken;
            var result = await _accountService.GetUser();
            AppUser user = (AppUser)result.items;
            result = await _userPricingService.GetAsync(pricingId);
            if(user.UserPricingId == pricingId)
            {
                TempData["Mail"] = "Your package is also this";
                return RedirectToAction("index","home");
            }
            var data = new BookPurchaseVM
            {
                PricingId = pricingId,
                AppUserId = userId,
                Description = "Hellow man",
                Title = "This is title",
                Price = ((UserPricing)result.itemView).Price,
                Nonce = ""
            };
            return View(data);

        }
        public async Task<IActionResult> Payment(double amount,int carId,string userId)
        {
            int i = 0;
            var gateway = _braintreeService.GetGateway();
            var clientToken = gateway.ClientToken.Generate();
            ViewBag.ClientToken = clientToken;
            Book book = new Book
            {
                CarId = carId,
                AppUserId= userId,
                Description= "Hellow man",
                CreatedAt = DateTime.UtcNow,
                Price= amount,
                Title = "This is title",
            };
            if (i == 0)
            {
                await _bookService.CreateAsync(book);
                i++;
            }

            var result = await _bookService.GetAllAsync(0, 0, x => x.CarId == carId);
            int BookId = ((IEnumerable<Book>)result.items).FirstOrDefault().Id;
            var data = new BookPurchaseVM
            {
                Id = BookId,
                AppUserId =userId,
                CarId = carId,
                Description = "Hellow man",
                Title = "This is title",
                Price = amount,
                Nonce = ""
            };
            return View(data);

        }
        [HttpPost]
        public async Task<IActionResult> Create(BookPurchaseVM model,double amount,string userId, int carId = 0,int bookId = 0,int pricingId =0)
        {
            CarUpdateDto dto = new CarUpdateDto();
            var results =new ApiResponse();
            Car car = new Car();
            if (carId !=0)
            {
                results = await _carService.GetAsync(model.CarId);
                dto = (CarUpdateDto)results.items;
                car = (Car)results.itemView;
            }
            var gateway = _braintreeService.GetGateway();
            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal(amount),
                PaymentMethodNonce = model.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                },
            };

            Result<Transaction> result = await gateway.Transaction.SaleAsync(request);

            if (result.IsSuccess())
            {
                results = await _accountService.GetUserById(userId);
                AppUser user = (AppUser)results.items;
                if (carId != 0)
                {
                    dto.StatusId = 3;
                    await _carService.UpdateAsync(carId, dto);
                    await _emailService.SendCarMail("nyhhat@gmail.com", user.Email, "Winning Car Receipt", bookId, user.Name + " " + user.Surname, car.UpdatedAt.ToString(), car);
                }
                else
                {
                    UpdateDto updateUser = new UpdateDto() {
                      UserPricingId = pricingId
                    };
                    await _accountService.Update(updateUser, null);
                }
                return RedirectToAction("Success", "Payment");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
