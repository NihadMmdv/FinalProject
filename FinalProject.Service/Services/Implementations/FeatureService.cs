using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Dtos.Categories;
using FinalProject.Service.Dtos.Features;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class FeatureService : IFeatureService
    {
        private readonly IGenericRepository<Feature> _repository;
        private readonly IMapper _mapper;

        public FeatureService(IGenericRepository<Feature> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(FeaturePostDto dto)
        {
            Feature Feature = _mapper.Map<Feature>(dto);
            await _repository.AddAsync(Feature);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Feature
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Feature> Features = await _repository.GetAllAsync(x => !x.IsDeleted,count,page,"UserPricing");
            return new ApiResponse
            {
                items = Features,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Feature Feature = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (Feature is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            FeatureUpdateDto dto = _mapper.Map<FeatureUpdateDto>(Feature);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Feature Feature = await _repository.GetAsync(x => x.Id == id);
            if (Feature is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Feature.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Feature
            };
        }
        public async Task<ApiResponse> UpdateAsync(int id, FeatureUpdateDto dto)
        {
            Feature Feature = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Feature is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Feature.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Feature.Name = dto.Name;
            Feature.Icon = dto.Icon;
            Feature.UserPricingId = dto.UserPricingId;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Feature
            };
        }
    }
}
