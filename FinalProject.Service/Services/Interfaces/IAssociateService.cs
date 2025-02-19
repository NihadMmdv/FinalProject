using FinalProject.Service.Dtos.Associates;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface IAssociateService
    {
        public Task<ApiResponse> CreateAsync(AssociatePostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync(int count, int page);
        public Task<ApiResponse> UpdateAsync(int id, AssociateUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
