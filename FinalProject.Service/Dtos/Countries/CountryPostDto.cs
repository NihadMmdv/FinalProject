using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Dtos.Countries
{
    public class CountryPostDto
    {
        public string Name { get; set; }
        public IFormFile file { get; set; }

    }
}
