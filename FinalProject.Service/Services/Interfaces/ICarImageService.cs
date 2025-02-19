using FinalProject.DAL.Entities;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface ICarImageService
    {
        public Task<ApiResponse> GetAsync(int id, Expression<Func<CarImage, bool>>? expression);
        public Task<ApiResponse> GetAllAsync(int count,int page, Expression<Func<CarImage, bool>>? expression);
        public Task<ApiResponse> Save();

    }
}
