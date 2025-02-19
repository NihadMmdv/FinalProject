using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Comments;
using FinalProject.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<ApiResponse> CreateAsync(CommentPostDto dto);
        public Task<ApiResponse> GetAsync(int id);
        public Task<ApiResponse> GetAllAsync(int count, int page, Expression<Func<Comment, bool>>? expression);
        public Task<ApiResponse> RemoveAsync(int id);
    }
}
