using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Entities
{
    public class Model:BaseEntity
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<Car> Cars { get; set; } 
    }
}
