using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Dtos.Categories;
using FinalProject.Service.Dtos.Models;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class ModelService : IModelService
    {
        private readonly IGenericRepository<Model> _repository;
        private readonly IMapper _mapper;

        public ModelService(IGenericRepository<Model> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(ModelPostDto dto)
        {
            Model Model = _mapper.Map<Model>(dto);
            await _repository.AddAsync(Model);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Model
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Model> Models = await _repository.GetAllAsync(x => !x.IsDeleted,count,page,"Brand");
            return new ApiResponse
            {
                items = Models.OrderBy(x => x.Name),
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Model Model = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (Model is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            ModelUpdateDto dto = _mapper.Map<ModelUpdateDto>(Model);

            return new ApiResponse
            {
                StatusCode = 200,
                items = dto
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Model Model = await _repository.GetAsync(x => x.Id == id);
            if (Model is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Model.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Model
            };
        }
        public async Task<ApiResponse> UpdateAsync(int id, ModelUpdateDto dto)
        {
            Model Model = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (Model is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Model.UpdatedAt = DateTime.UtcNow.AddHours(4);
            Model.Name = dto.Name;
            Model.BrandId = dto.BrandId;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Model
            };
        }
    }
}
