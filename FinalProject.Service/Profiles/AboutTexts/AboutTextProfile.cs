using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.AboutTexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.AboutTexts
{
    public class AboutTextProfile:Profile
    {
        public AboutTextProfile()
        {
            CreateMap<AboutTextPostDto, AboutText>();
            CreateMap<AboutTextUpdateDto, AboutText>();
            CreateMap<AboutText, AboutTextUpdateDto>();
        }
    }
}
