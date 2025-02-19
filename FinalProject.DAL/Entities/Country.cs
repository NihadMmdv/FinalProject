using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Entities
{
    public class Country:BaseEntity
    {
        public string Name { get; set; }
        public string FlagImage { get; set; }
        public string FlagUrl { get; set; }
        public List<AppUser>? AppUsers { get; set; }
    }
}
