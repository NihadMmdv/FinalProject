using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Features;

namespace FinalProject.Service.Profiles.Features
{
    public class FeatureProfile:Profile
    {
        public FeatureProfile() {
            CreateMap<FeaturePostDto, Feature>();
            CreateMap<FeatureUpdateDto, Feature>();
            CreateMap<Feature, FeatureUpdateDto>();
        }
    }
}
