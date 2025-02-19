using FinalProject.Service.Dtos.Tags;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface ITagService
    {
        public Task<ApiResponse> CreateAsync(TagPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync(int count, int page);
        public Task<ApiResponse> UpdateAsync(int id, TagUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
