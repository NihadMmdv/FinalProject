using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.Blogs
{
    public class BlogProfile:Profile
    {
        public BlogProfile()
        {
            CreateMap<BlogPostDto, Blog>();
            CreateMap<BlogUpdateDto, Blog>();
            CreateMap<Blog, BlogUpdateDto>();
        }
    }
}
