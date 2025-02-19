using FinalProject.Service.Dtos.Messages;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<ApiResponse> CreateAsync(MessagePostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync(int count,int page);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
