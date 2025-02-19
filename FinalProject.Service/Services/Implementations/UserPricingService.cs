using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Dtos.UserPricings;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class UserPricingService : IUserPricingService
    {
        private readonly IGenericRepository<UserPricing> _repository;
        private readonly IMapper _mapper;

        public UserPricingService(IGenericRepository<UserPricing> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(UserPricingPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && !x.IsDeleted))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            UserPricing UserPricing = _mapper.Map<UserPricing>(dto);
            await _repository.AddAsync(UserPricing);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = UserPricing
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<UserPricing> UserPricings = await _repository.GetAllAsync(x => !x.IsDeleted,count,page,"Features");
            return new ApiResponse
            {
                items = UserPricings,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            UserPricing UserPricing = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id, "Features");
            if (UserPricing is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            UserPricingUpdateDto dto = _mapper.Map<UserPricingUpdateDto>(UserPricing);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto,
                itemView = UserPricing
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            UserPricing UserPricing = await _repository.GetAsync(x => x.Id == id);
            if (UserPricing is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            UserPricing.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = UserPricing
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, UserPricingUpdateDto dto)
        {
            
            UserPricing UserPricing = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (dto.Name != UserPricing.Name)
            {
                if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower() && !x.IsDeleted))
                {
                    return new ApiResponse
                    {
                        StatusCode = 400,
                        Description = $"{dto.Name} Already exists"
                    };
                }
            }
            if (UserPricing is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            UserPricing.UpdatedAt = DateTime.UtcNow.AddHours(4);
            UserPricing.Name = dto.Name;
			UserPricing.Price = dto.Price;
			await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = UserPricing
            };
        }
    }
}
