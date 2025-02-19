using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Bids;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface IBidService
    {
        public Task<ApiResponse> CreateAsync(BidPostDto dto);
        public Task<ApiResponse> GetAsync(Expression<Func<Bid, bool>> expression);
        public Task<ApiResponse> GetAllAsync(int count,int page, Expression<Func<Bid, bool>>? expression);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
