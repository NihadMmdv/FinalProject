﻿using FluentValidation;
using FinalProject.Service.Dtos.AboutTexts;
using FinalProject.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.AboutTexts
{
    public class AboutTextPostDtoValidation:AbstractValidator<AboutTextPostDto>
    {
        public AboutTextPostDtoValidation()
        {
            RuleFor(x => x.Title)
               .NotEmpty()
               .NotNull()
               .MinimumLength(3)
               .MaximumLength(20);
			RuleFor(x => x.SubTitle)
		.NotEmpty()
		.NotNull()
		.MinimumLength(3)
		.MaximumLength(50);

			RuleFor(x => x.Text)
           	.NotEmpty()
	          .NotNull()
	            .MinimumLength(3)
	         .MaximumLength(500);
		}
    }
}
