using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Dtos.Categories;
using FinalProject.Service.Dtos.Positions;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class PositionService : IPositionService
    {
        private readonly IGenericRepository<Position> _repository;
        private readonly IMapper _mapper;

        public PositionService(IGenericRepository<Position> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(PositionPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Position Position = _mapper.Map<Position>(dto);
            await _repository.AddAsync(Position);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Position
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Position> Positions = await _repository.GetAllAsync(x => !x.IsDeleted,count,page);
            return new ApiResponse
            {
                items = Positions,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Position Position = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (Position is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            PositionUpdateDto dto = _mapper.Map<PositionUpdateDto>(Position);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Position Position = await _repository.GetAsync(x => x.Id == id);
            if (Position is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Position.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Position
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, PositionUpdateDto dto)
        {
           
            Position Position = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (dto.Name.ToLower() != Position.Name.ToLower())
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
            if (Position is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Position.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Position.Name = dto.Name;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Position
            };
        }
    }
}
