using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Dtos.Colors;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class ColorService : IColorService
    {
        private readonly IGenericRepository<Color> _repository;
        private readonly IMapper _mapper;

        public ColorService(IGenericRepository<Color> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(ColorPostDto dto)
        {
            if (await _repository.isExsist(x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower()))
            {
                return new ApiResponse
                {
                    StatusCode = 400,
                    Description = $"{dto.Name} Already exists"
                };
            }
            Color Color = _mapper.Map<Color>(dto);
            await _repository.AddAsync(Color);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Color
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Color> Colors = await _repository.GetAllAsync(x => !x.IsDeleted,count,page);
            return new ApiResponse
            {
                items = Colors.OrderBy(x => x.Name),
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Color Color = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (Color is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            ColorUpdateDto dto = _mapper.Map<ColorUpdateDto>(Color);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Color Color = await _repository.GetAsync(x => x.Id == id);
            if (Color is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Color.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Color
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, ColorUpdateDto dto)
        {
           
            Color Color = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Color.Name.ToLower() != dto.Name.ToLower())
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
            if (Color is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Color.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Color.Name = dto.Name;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Color
            };
        }
    }
}
