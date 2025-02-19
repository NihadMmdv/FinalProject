﻿using Microsoft.AspNetCore.Http;
using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Dtos.Brands
{
    public class BrandUpdateDto
    {
		public string Name { get; set; }
		public IFormFile? file { get; set; }
	}
}
