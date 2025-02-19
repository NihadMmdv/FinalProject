using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Blogs;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface IBlogService
    {
        public Task<ApiResponse> CreateAsync(BlogPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync(int count,int page, Expression<Func<Blog, bool>>? expression);
        public Task<ApiResponse> UpdateAsync(int id, BlogUpdateDto dto);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
