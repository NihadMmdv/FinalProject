using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Brands;

namespace FinalProject.Service.Profiles.Brands
{
    public class BrandProfile:Profile
    {
        public BrandProfile() {
            CreateMap<BrandPostDto, Brand>();
            CreateMap<BrandUpdateDto, Brand>();
            CreateMap<Brand, BrandUpdateDto>();
        }
    }
}
