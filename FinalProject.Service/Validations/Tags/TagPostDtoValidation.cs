using FluentValidation;
using FinalProject.Service.Dtos.Categories;
using FinalProject.Service.Dtos.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.Tags
{
    public class TagPostDtoValidation:AbstractValidator<TagPostDto>
    {
        public TagPostDtoValidation()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .NotNull().WithMessage("Name can not be null")
               .MinimumLength(3)
               .MaximumLength(30);
        }
    }
}
