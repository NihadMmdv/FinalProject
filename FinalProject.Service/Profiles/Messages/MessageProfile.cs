using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.Messages
{
    public class MessageProfile:Profile
    {
        public MessageProfile()
        {
			CreateMap<MessagePostDto, Message>();
        }
    }
}
