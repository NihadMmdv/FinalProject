using FluentValidation;
using FinalProject.Service.Dtos.Subscribes;
using FinalProject.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinalProject.Service.Validations.Subscribes
{
    public class SubscribePostDtoValidation:AbstractValidator<SubscribePostDto>
    {
        public SubscribePostDtoValidation()
        {
            RuleFor(x => x)
                  .Custom((x, context) =>
                  {
                      if (x.Email != null)
                      {
                          Regex re = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
                          if (!re.IsMatch(x.Email))
                          {
                              context.AddFailure("", "Email format must be correct");
                          }
                      }
                  }
               );
        }
    }
}

