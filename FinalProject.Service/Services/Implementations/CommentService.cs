using AutoMapper;
using Braintree;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Data;
using FinalProject.Service.Dtos.Comments;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Repository.Interface;

namespace FinalProject.Service.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> _repository;
        private readonly IMapper _mapper;
    

        public CommentService(IGenericRepository<Comment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(CommentPostDto dto)
        {
            Comment Comment = _mapper.Map<Comment>(dto);
            await _repository.AddAsync(Comment);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Comment
            };
        }
        public async Task<ApiResponse> GetAllAsync(int count,int page, Expression<Func<Comment, bool>>? expression)
        {
            IEnumerable<Comment> Comments = new List<Comment>();
            if (expression is null)
            {
                Comments = await _repository.GetAllAsync(x => !x.IsDeleted, count, page, "AppUser");
            }
            else
            {
                Comments = await _repository.GetAllAsync(expression, count, page, "AppUser");
            }
            return new ApiResponse
            {
                items = Comments,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Comment Comment = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id, "AppUser");
            if (Comment is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            return new ApiResponse
            {
                StatusCode = 200,
                items = Comment
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Comment Comment = await _repository.GetAsync(x => x.Id == id);
            if (Comment is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Comment.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Comment
            };
        }
    }
}
