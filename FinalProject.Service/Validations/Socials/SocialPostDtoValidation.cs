using FluentValidation;
using FinalProject.Service.Dtos.Socials;
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
    public class SocialPostDtoValidation:AbstractValidator<SocialPostDto>
    {
        public SocialPostDtoValidation()
        {
            RuleFor(x => x.Icon)
               .NotEmpty()
               .NotNull();
			RuleFor(x => x.Link)
	           .NotEmpty()
	           .NotNull();
		}
    }
}

