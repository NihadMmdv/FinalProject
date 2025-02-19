using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Subscribes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.Subscribes
{
    public class SubscribeProfile:Profile
    {
        public SubscribeProfile()
        {
            CreateMap<SubscribePostDto, Subscribe>();
        }
    }
}
