using AutoMapper;
using FinalProject.DAL.Entities;
using FinalProject.Service.Dtos.Bids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Profiles.Bids
{
    public class BidProfile:Profile
    {
        public BidProfile()
        {
			CreateMap<BidPostDto, Bid>();
        }
    }
}
