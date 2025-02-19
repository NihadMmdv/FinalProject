using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Bans;

namespace FinalProject.Service.Profiles.Bans
{
    public class BanProfile:Profile
    {
        public BanProfile() {
            CreateMap<BanPostDto, Ban>();
            CreateMap<BanUpdateDto, Ban>();
            CreateMap<Ban,BanUpdateDto>();
        }
    }
}
