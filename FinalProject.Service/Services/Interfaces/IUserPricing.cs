﻿using FinalProject.Service.Dtos.UserPricings;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface IUserPricingService
    {
        public Task<ApiResponse> CreateAsync(UserPricingPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync(int count, int page);
        public Task<ApiResponse> UpdateAsync(int id, UserPricingUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
