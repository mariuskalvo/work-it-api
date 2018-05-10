using System;
using System.Collections.Generic;
using System.Text;
using Core.DTOs;
using FluentValidation;

namespace Core.Validators
{
    class CreateGroupDtoValidator : AbstractValidator<CreateGroupDto>
    {
        public CreateGroupDtoValidator()
        {
            RuleFor(g => g.Title).NotNull().NotEmpty();
        }
    }
}
