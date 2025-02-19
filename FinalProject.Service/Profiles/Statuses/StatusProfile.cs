using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Statuses;

namespace FinalProject.Service.Profiles.Statuses
{
    public class StatusProfile:Profile
    {
        public StatusProfile() {
            CreateMap<StatusPostDto, Status>();
            CreateMap<StatusUpdateDto, Status>();
            CreateMap<Status,StatusUpdateDto>();
        }
    }
}
