﻿using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL.Entities
{
	public class Associate:BaseEntity
	{
		public string Name { get; set; }
		public string Image { get; set; }
		public string ImageUrl { get; set; }
	}
}
