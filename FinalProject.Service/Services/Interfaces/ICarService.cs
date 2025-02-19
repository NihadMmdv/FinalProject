using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Cars;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface ICarService
    {
        public Task<ApiResponse> CreateAsync(CarPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync(int count,int page, Expression<Func<Car, bool>>? expression);
        public Task<ApiResponse> UpdateAsync(int id, CarUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
