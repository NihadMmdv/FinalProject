using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Service.Dtos.Categories;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Fuels;

namespace FinalProject.Service.Profiles.Fuels
{
    public class FuelProfile:Profile
    {
        public FuelProfile() {
            CreateMap<FuelPostDto, Fuel>();
            CreateMap<FuelUpdateDto, Fuel>();
            CreateMap<Fuel,FuelUpdateDto>();
        }
    }
}
