using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.TextWhies;

namespace FinalProject.Service.Profiles.TextWhies
{
    public class TextWhyProfile:Profile
    {
        public TextWhyProfile() {
            CreateMap<TextWhyPostDto, TextWhy>();
            CreateMap<TextWhyUpdateDto, TextWhy>();
            CreateMap<TextWhy, TextWhyUpdateDto>();
        }
    }
}
