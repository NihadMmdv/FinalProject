﻿using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.Service.Dtos.Messages;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IGenericRepository<Message> _repository;
        private readonly IMapper _mapper;

        public MessageService(IGenericRepository<Message> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(MessagePostDto dto)
        {
            Message Message = _mapper.Map<Message>(dto);
            await _repository.AddAsync(Message);
            await _repository.SaveAsync();
            Message.CreatedAt = DateTime.Now;
            return new ApiResponse
            {
                StatusCode = 201,
                items = Message
            };
        }

        public async Task<ApiResponse> GetAllAsync(int count,int page)
        {
            IEnumerable<Message> messages = await _repository.GetAllAsync(x => !x.IsDeleted,count,page);
            return new ApiResponse
            {
                items = messages,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Message Message = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (Message is null)
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
                items = Message
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Message Message = await _repository.GetAsync(x => x.Id == id);
            if (Message is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Message.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Message
            };
        }
    }
}
