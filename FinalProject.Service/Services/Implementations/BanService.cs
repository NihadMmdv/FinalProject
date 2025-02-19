using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Dtos.Bans;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class BanService : IBanService
    {
        private readonly IGenericRepository<Ban> _repository;
        private readonly IMapper _mapper;

        public BanService(IGenericRepository<Ban> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(BanPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Ban Ban = _mapper.Map<Ban>(dto);
            await _repository.AddAsync(Ban);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Ban
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Ban> Bans = await _repository.GetAllAsync(x => !x.IsDeleted,count,page,"Cars");
            return new ApiResponse
            {
                items = Bans.OrderBy(x => x.Name),
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Ban Ban = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (Ban is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            BanUpdateDto dto = _mapper.Map<BanUpdateDto>(Ban);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Ban Ban = await _repository.GetAsync(x => x.Id == id);
            if (Ban is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Ban.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Ban
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, BanUpdateDto dto)
        {
           
            Ban Ban = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Ban.Name.ToLower() != dto.Name.ToLower())
            {
                if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
                {
                    return new ApiResponse
                    {
                        StatusCode = 400,
                        Description = $"{dto.Name} Already exists"
                    };
                }
            }
            if (Ban is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Ban.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Ban.Name = dto.Name;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Ban
            };
        }
    }
}
