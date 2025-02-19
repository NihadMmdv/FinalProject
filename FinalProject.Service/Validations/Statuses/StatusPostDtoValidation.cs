using FluentValidation;
using FinalProject.Service.Dtos.Statuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.Statuses
{
    public class StatusPostDtoValidation:AbstractValidator<StatusPostDto>
    {
        public StatusPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(2)
               .MaximumLength(40);
        }
    }
}
