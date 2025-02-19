using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly IMapper _mapper;

        public BookService(IGenericRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(Book book)
        {
            await _repository.AddAsync(book);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = book
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page, Expression<Func<Book, bool>>? expression)
        {
            IEnumerable<Book> Books = await _repository.GetAllAsync(expression,count,page);
            return new ApiResponse
            {
                items = Books,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Book Book = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (Book is null)
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
                items = Book
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Book Book = await _repository.GetAsync(x => x.Id == id);
            if (Book is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Book.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Book
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, Book book)
        {
            Book Book = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Book is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Book.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Book.AppUserId = book.AppUserId;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Book
            };
        }
    }
}
