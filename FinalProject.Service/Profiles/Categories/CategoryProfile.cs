using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Service.Dtos.Categories;
using FinalProject.DAL.Entities;

namespace FinalProject.Service.Profiles.Categories
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile() {
            CreateMap<CategoryPostDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();
        }
    }
}
