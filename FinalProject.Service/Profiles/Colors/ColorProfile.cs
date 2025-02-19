using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Service.Dtos.Categories;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Colors;

namespace FinalProject.Service.Profiles.Colors
{
    public class ColorProfile:Profile
    {
        public ColorProfile() {
            CreateMap<ColorPostDto, Color>();
            CreateMap<ColorUpdateDto, Color>();
            CreateMap<Color,ColorUpdateDto>();
        }
    }
}
