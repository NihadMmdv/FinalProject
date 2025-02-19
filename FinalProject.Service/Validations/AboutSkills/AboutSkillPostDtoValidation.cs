using FluentValidation;
using FinalProject.Service.Dtos.AboutSkills;
using FinalProject.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.AboutSkills
{
    public class AboutSkillPostDtoValidation:AbstractValidator<AboutSkillPostDto>
    {
        public AboutSkillPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null");
	
            RuleFor(x => x)
            .Custom((x, context) =>
            {
                if (x.Degree != null)
                {
                    if (x.Degree >100 || x.Degree<0)
                    {
                        context.AddFailure("degree", "Degree value must between 0 and 100");
                    }
                }
            }
            );
        }
    }
}
