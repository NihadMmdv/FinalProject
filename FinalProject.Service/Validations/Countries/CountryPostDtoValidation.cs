﻿using FluentValidation;
using FinalProject.Service.Dtos.Countries;
using FinalProject.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.Countrys
{
    public class CountryPostDtoValidation:AbstractValidator<CountryPostDto>
    {
        public CountryPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(40);
            RuleFor(x => x)
            .Custom((x, context) =>
            {
                if (x.file != null)
                {
                    if (!Helper.isImage(x.file))
                    {
                        context.AddFailure("file", "The type of file must be image");
                    }
                    if (!Helper.isSizeOk(x.file, 2))
                    {
                        context.AddFailure("file", "The size of image less than 2 mb");
                    }
                }
            });
        }
    }
}
