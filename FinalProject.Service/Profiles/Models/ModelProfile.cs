using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.Models
{
    public class ModelProfile:Profile
    {
        public ModelProfile()
        {
            CreateMap<ModelPostDto, Model>();
            CreateMap<ModelUpdateDto, Model>();
            CreateMap<Model, ModelUpdateDto>();
        }
    }
}
