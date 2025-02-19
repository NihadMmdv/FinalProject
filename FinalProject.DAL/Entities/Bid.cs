using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Entities
{
    public class Bid:BaseEntity
    {
        public int CarId { get; set; }
        public double Count { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
