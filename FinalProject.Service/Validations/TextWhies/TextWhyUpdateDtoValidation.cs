using FluentValidation;
using FinalProject.Service.Dtos.TextWhies;
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
    public class TextWhyUpdateDtoValidation:AbstractValidator<TextWhyUpdateDto>
    {
        public TextWhyUpdateDtoValidation()
        {
            RuleFor(x => x.Title)
                 .NotEmpty()
                 .NotNull().WithMessage("Title can not be null")
                 .MinimumLength(3)
                 .MaximumLength(20);
            RuleFor(x => x.Text)
            .NotEmpty()
            .NotNull().WithMessage("Text can not be null")
            .MinimumLength(10)
            .MaximumLength(100);
            RuleFor(x => x.Text)
           .NotEmpty()
           .NotNull();
            RuleFor(x => x.SettingId).
                NotNull();
        }
    }
}

