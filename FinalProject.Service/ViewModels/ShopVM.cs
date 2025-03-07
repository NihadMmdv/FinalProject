﻿using FinalProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.ViewModels
{
    public class ShopVM
    {
        public Car Car { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public Country Country { get; set; }
    }
}
