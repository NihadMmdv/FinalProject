using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Sliders;

namespace FinalProject.Service.Profiles.Sliders
{
    public class SliderProfile:Profile
    {
        public SliderProfile() {
            CreateMap<SliderPostDto, Slider>();
            CreateMap<SliderUpdateDto, Slider>();
            CreateMap<Slider, SliderUpdateDto>();
        }
    }
}
