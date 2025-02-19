using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Dtos.Categories;
using FinalProject.Service.Dtos.TextWhies;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class TextWhyService : ITextWhyService
    {
        private readonly IGenericRepository<TextWhy> _repository;
        private readonly IMapper _mapper;

        public TextWhyService(IGenericRepository<TextWhy> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(TextWhyPostDto dto)
        {
            TextWhy TextWhy = _mapper.Map<TextWhy>(dto);
            await _repository.AddAsync(TextWhy);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = TextWhy
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<TextWhy> TextWhys = await _repository.GetAllAsync(x => !x.IsDeleted,count,page);
            return new ApiResponse
            {
                items = TextWhys,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            TextWhy TextWhy = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (TextWhy is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            TextWhyUpdateDto dto = _mapper.Map<TextWhyUpdateDto>(TextWhy);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            TextWhy TextWhy = await _repository.GetAsync(x => x.Id == id);
            if (TextWhy is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            TextWhy.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = TextWhy
            };
        }
        public async Task<ApiResponse> UpdateAsync(int id, TextWhyUpdateDto dto)
        {
            TextWhy TextWhy = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (TextWhy is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            TextWhy.UpdatedAt = DateTime.UtcNow.AddHours(4);
            TextWhy.Title = dto.Title;
            TextWhy.Text = dto.Text;
            TextWhy.Icon = dto.Icon;
            TextWhy.SettingId = dto.SettingId;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = TextWhy
            };
        }
    }
}
