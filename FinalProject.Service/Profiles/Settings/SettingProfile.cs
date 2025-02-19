using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.Settings
{
    public class SettingProfile:Profile
    {
        public SettingProfile()
        {
            CreateMap<SettingPostDto, Setting>();
            CreateMap<SettingUpdateDto, Setting>();
            CreateMap<Setting, SettingUpdateDto>();
        }
    }
}
