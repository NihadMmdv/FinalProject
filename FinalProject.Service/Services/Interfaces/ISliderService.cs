using FinalProject.Service.Dtos.Sliders;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface ISliderService
    {
        public Task<ApiResponse> CreateAsync(SliderPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync(int count, int page);
        public Task<ApiResponse> UpdateAsync(int id, SliderUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
