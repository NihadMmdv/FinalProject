using FinalProject.Service.Dtos.Subscribes;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface ISubscribeService
    {
        public Task<ApiResponse> CreateAsync(SubscribePostDto dto);
        public Task<ApiResponse> GetAllAsync(int count,int page);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
