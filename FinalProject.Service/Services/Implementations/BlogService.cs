using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.Service.Dtos.Blogs;
using FinalProject.Service.Responses;
using FinalProject.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using FinalProject.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAL.Data;
using System.Linq.Expressions;
using FinalProject.DAL.Repository.Interface;

namespace FinalProject.Service.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly IGenericRepository<Blog> _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _evn;
        private readonly IHttpContextAccessor _http;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly IGenericRepository<Tag> _tagRepository;
        private readonly IGenericRepository<BlogCategory> _blogCategoryRepository;
        private readonly IGenericRepository<BlogTag> _blogTagRepository;
        private readonly CustomDBContext _context;
		public BlogService(IGenericRepository<Blog> repository, IMapper mapper, IWebHostEnvironment evn, IHttpContextAccessor http, IGenericRepository<Category> categoryRepository, IGenericRepository<Tag> tagRepository, IGenericRepository<BlogTag> blogTagRepository, IGenericRepository<BlogCategory> blogCategoryRepository, CustomDBContext context)
		{
			_repository = repository;
			_mapper = mapper;
			_evn = evn;
			_http = http;
			_categoryRepository = categoryRepository;
			_tagRepository = tagRepository;
			_blogTagRepository = blogTagRepository;
			_blogCategoryRepository = blogCategoryRepository;
			_context = context;
		}
		public async Task<ApiResponse> CreateAsync(BlogPostDto dto)
        {
            Blog Blog = _mapper.Map<Blog>(dto);
            Blog.Image = dto.file.CreateImage(_evn.WebRootPath, "Images/Blogs");
            Blog.ImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
                + $"assests/images/books/{Blog.Image}";
            Blog.AuthorImage = dto.fileAuthor.CreateImage(_evn.WebRootPath, "Images/Blogs/Authors");
            foreach (var item in dto.CategoryIds)
            {
                if (!await _categoryRepository.isExsist(x => x.Id == item))
                {
                    return new ApiResponse
                    {
                        StatusCode = 404,
                        Description = "Invalid Category Id"
                    };
                }
                BlogCategory blogCategory = new BlogCategory
                {
                    CreatedAt = DateTime.Now,
                    Blog = Blog,
                    CategoryId = item
                };
                await _blogCategoryRepository.AddAsync(blogCategory);
                Blog.BlogCategories?.Add(blogCategory);
            }
            foreach (var item in dto.TagIds)
            {
                if (!await _tagRepository.isExsist(x => x.Id == item))
                {
                    return new ApiResponse
                    {
                        StatusCode = 404,
                        Description = "Invalid Tag Id"
                    };
                }
                BlogTag blogTag = new BlogTag
                {
                    CreatedAt = DateTime.Now,
                    Blog = Blog,
                    TagId = item
                };
                await _blogTagRepository.AddAsync(blogTag);
                Blog.BlogTags?.Add(blogTag);
            }
            await _repository.AddAsync(Blog);
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 201,
                items = Blog
            };
        }
        public async Task<ApiResponse> GetAllAsync(int count,int page, Expression<Func<Blog, bool>>? expression)
        {
            IEnumerable<Blog> blogs = new List<Blog>();
            if (expression is null)
            {
               blogs = await _repository.GetAllAsync(x => !x.IsDeleted, count, page, "BlogCategories","Comments");
            }
            else
            {
               blogs = await _repository.GetAllAsync(expression, count, page, "BlogCategories", "Comments");

            }
            return new ApiResponse
            {
                items = blogs,
                StatusCode = 200
            };
        }

        public async Task<ApiResponse> GetAsync(int id)
        {
            Blog Blog = await _repository.GetAsync(x => !x.IsDeleted && x.Id == id, "BlogCategories","BlogTags", "Comments");
            if (Blog is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            BlogUpdateDto dto = _mapper.Map<BlogUpdateDto>(Blog);
			foreach (var item in Blog.BlogCategories)
			{
				dto.CategoryIds?.Add(item.CategoryId);
			}
			foreach (var item in Blog.BlogTags)
			{
				dto.TagIds?.Add(item.TagId);
			}
			return new ApiResponse
            {
                StatusCode = 200,
                items = dto,
                itemView = Blog
            };
        }

        public async Task<ApiResponse> RemoveAsync(int id)
        {
            Blog Blog = await _repository.GetAsync(x => x.Id == id);
            if (Blog is null)
            {
                return new ApiResponse
                {
                    StatusCode = 404,
                    Description = "Not found"
                };
            }
            Blog.IsDeleted = true;
            await _repository.SaveAsync();
            return new ApiResponse
            {
                StatusCode = 200,
                items = Blog
            };
        }

        public async Task<ApiResponse> UpdateAsync(int id, BlogUpdateDto dto)
        {
			Blog Blog = await _repository.GetAsync(x => x.Id == id && !x.IsDeleted, "BlogCategories", "BlogTags");
			if (Blog is null)
			{
				return new ApiResponse
				{
					StatusCode = 404,
					Description = "Not found"
				};
			}
			if (dto.file is not null)
			{
				Blog.Image = dto.file.CreateImage(_evn.WebRootPath, "Images/Blogs");
				Blog.ImageUrl = _http.HttpContext?.Request.Scheme + "://" + _http.HttpContext?.Request.Host
					+ $"assests/images/books/{Blog.Image}";
			}
            if(dto.fileAuthor is not null)
            {
				Blog.AuthorImage = dto.fileAuthor.CreateImage(_evn.WebRootPath, "Images/Blogs/Authors");
			}

			List<BlogCategory> RemoveableCategory = await _context.BlogCategories.
			  Where(x => !dto.CategoryIds.Contains(x.CategoryId) && x.BlogId == Blog.Id).ToListAsync();

			_context.BlogCategories.RemoveRange(RemoveableCategory);
			foreach (var item in dto.CategoryIds)
			{
				if (_context.BlogCategories.Where(x => x.BlogId == id && x.CategoryId == item).Count() > 0)
					continue;

				await _blogCategoryRepository.AddAsync(new BlogCategory
				{
					BlogId = id,
					CategoryId = item
				});
			}
			Blog.UpdatedAt = DateTime.UtcNow.AddHours(4);
			Blog.Title = dto.Title;
			Blog.Description = dto.Description;
			Blog.Comment = dto.Comment;
			Blog.Author = dto.Author;
			await _repository.SaveAsync();
			return new ApiResponse
			{
				StatusCode = 200,
				items = Blog
			};

		}
    }
}
