using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Associates;

namespace FinalProject.Service.Profiles.Associates
{
    public class AssociateProfile:Profile
    {
        public AssociateProfile() {
            CreateMap<AssociatePostDto, Associate>();
            CreateMap<AssociateUpdateDto, Associate>();
            CreateMap<Associate, AssociateUpdateDto>();
        }
    }
}
