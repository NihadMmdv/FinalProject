﻿using FluentValidation;
using FinalProject.Service.Dtos.Models;
using FinalProject.Service.Dtos.Messages;
using FinalProject.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.Messages
{
    public class ModelPostDtoValidation:AbstractValidator<ModelPostDto>
    {
        public ModelPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(50);
            RuleFor(x => x.BrandId).
                NotNull();
        }
    }
}

