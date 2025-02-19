using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Service.Dtos.Categories;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.UserPricings;

namespace FinalProject.Service.Profiles.UserPricings
{
    public class UserPricingProfile:Profile
    {
        public UserPricingProfile() {
			CreateMap<UserPricingPostDto, UserPricing>();
            CreateMap<UserPricingUpdateDto, UserPricing>();
            CreateMap<UserPricing,UserPricingUpdateDto>();
        }
    }
}
