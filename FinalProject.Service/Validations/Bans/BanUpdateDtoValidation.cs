using FluentValidation;
using FinalProject.Service.Dtos.Categories;
using FinalProject.Service.Dtos.Bans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.Bans
{
    public class BanUpdateDtoValidation:AbstractValidator<BanUpdateDto>
    {
        public BanUpdateDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(2)
               .MaximumLength(40);
        }
    }
}
