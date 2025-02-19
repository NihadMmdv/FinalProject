using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Settings;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Implementations;
using FinalProject.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot;
using Telegram.Bot.Types;
using ApiResponse = FinalProject.Service.Responses.ApiResponse;

namespace FinalProject.Service.Services.Interfaces
{
    public interface ISettingService
    {
        public Task<ApiResponse> CreateAsync(SettingPostDto dto);
        public Task<ApiResponse> GetAsync(int? id);
        public Task<ApiResponse> GetAllAsync(int count,int page);
        public Task<ApiResponse> UpdateAsync(int id, SettingUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
        public Task<SettingVM> GetSetting();
    }
}
