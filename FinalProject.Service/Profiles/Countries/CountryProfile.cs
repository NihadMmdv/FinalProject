using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Countries;

namespace FinalProject.Service.Profiles.Countrys
{
    public class CountryProfile:Profile
    {
        public CountryProfile() {
            CreateMap<CountryPostDto, Country>();
            CreateMap<CountryUpdateDto, Country>();
            CreateMap<Country, CountryUpdateDto>();
        }
    }
}
