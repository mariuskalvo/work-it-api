using System;
using System.Collections.Generic;
using System.Text;
using Core.DTOs;
using FluentValidation;

namespace Core.Validators
{
    class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(g => g.Title).NotNull().NotEmpty();
        }
    }
}
