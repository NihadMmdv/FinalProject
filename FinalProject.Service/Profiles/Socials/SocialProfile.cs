﻿using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Socials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.Socials
{
    public class SocialProfile:Profile
    {
        public SocialProfile()
        {
            CreateMap<SocialPostDto, Social>();
            CreateMap<SocialUpdateDto, Social>();
            CreateMap<Social, SocialUpdateDto>();
        }
    }
}
