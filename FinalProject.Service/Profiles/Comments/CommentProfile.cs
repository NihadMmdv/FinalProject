using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.Comments
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
			CreateMap<CommentPostDto, Comment>();
        }
    }
}
