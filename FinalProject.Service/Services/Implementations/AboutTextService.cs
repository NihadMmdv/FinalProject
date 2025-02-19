using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Dtos.Categories;
using FinalProject.Service.Dtos.AboutTexts;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class AboutTextService : IAboutTextService
    {
        private readonly IGenericRepository<AboutText> _repository;
        private readonly IMapper _mapper;

        public AboutTextService(IGenericRepository<AboutText> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(AboutTextPostDto dto)
        {
            AboutText AboutText = _mapper.Map<AboutText>(dto);
            await _repository.AddAsync(AboutText);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = AboutText
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<AboutText> AboutTexts = await _repository.GetAllAsync(x => !x.IsDeleted,count,page);
            return new ApiResponse
            {
                items = AboutTexts,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int? id)
        {
            AboutText AboutText = new AboutText();

            if (id == null)
            {
                AboutText = await _repository.GetAsync(x => !x.IsDeleted);
            }
            else
            {
                AboutText = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            }
            if (AboutText is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            AboutTextUpdateDto dto = _mapper.Map<AboutTextUpdateDto>(AboutText);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto,
                itemView = AboutText
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            AboutText AboutText = await _repository.GetAsync(x => x.Id == id);
            if (AboutText is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            AboutText.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = AboutText
            };
        }
        public async Task<ApiResponse> UpdateAsync(int id, AboutTextUpdateDto dto)
        {
            AboutText AboutText = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (AboutText is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            AboutText.UpdatedAt = DateTime.UtcNow.AddHours(4);
            AboutText.Title = dto.Title;
            AboutText.SubTitle = dto.SubTitle;
            AboutText.Text = dto.Text;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = AboutText
            };
        }
    }
}
