using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.AboutSkills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.AboutSkills
{
    public class AboutSkillProfile:Profile
    {
        public AboutSkillProfile()
        {
            CreateMap<AboutSkillPostDto, AboutSkill>();
            CreateMap<AboutSkillUpdateDto, AboutSkill>();
            CreateMap<AboutSkill, AboutSkillUpdateDto>();
        }
    }
}
