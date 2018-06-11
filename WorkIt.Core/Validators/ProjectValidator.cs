using System;
using System.Collections.Generic;
using System.Text;
using Core.DTOs;
using FluentValidation;
using WorkIt.Core.DTOs.Project;

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
