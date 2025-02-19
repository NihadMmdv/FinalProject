using FluentValidation;
using FinalProject.Service.Dtos.Categories;
using FinalProject.Service.Dtos.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.Positions
{
    public class PositionPostDtoValidation:AbstractValidator<PositionPostDto>
    {
        public PositionPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(30);
        }
    }
}
