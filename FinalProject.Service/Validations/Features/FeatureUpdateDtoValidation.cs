using FluentValidation;
using FinalProject.Service.Dtos.Features;
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
    public class FeatureUpdateDtoValidation:AbstractValidator<FeatureUpdateDto>
    {
        public FeatureUpdateDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(20);
            RuleFor(x => x.UserPricingId).
                NotNull();
        }
    }
}

