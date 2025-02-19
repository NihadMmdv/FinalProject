using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Service.Dtos.Categories;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Tags;

namespace FinalProject.Service.Profiles.Tags
{
    public class TagProfile:Profile
    {
        public TagProfile() {
            CreateMap<TagPostDto, Tag>();
            CreateMap<TagUpdateDto, Tag>();
            CreateMap<Tag,TagUpdateDto>();
        }
    }
}
