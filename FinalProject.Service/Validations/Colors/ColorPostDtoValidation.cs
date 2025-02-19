using FluentValidation;
using FinalProject.Service.Dtos.Colors;
using FinalProject.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.Colors
{
    public class ColorPostDtoValidation:AbstractValidator<ColorPostDto>
    {
        public ColorPostDtoValidation()
        {
            RuleFor(x => x.Name)
          .NotEmpty()
          .NotNull().WithMessage("Name can not be null")
          .MinimumLength(3)
          .MaximumLength(30);
        }
    }
}
