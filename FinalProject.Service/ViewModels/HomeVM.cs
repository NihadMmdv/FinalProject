using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<TextWhy> Why { get; set; }
        public IEnumerable<Associate> Associates { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public Setting Setting { get; set; }
    }
}
