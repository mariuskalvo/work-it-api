using System;
using System.Collections.Generic;
using System.Text;
using Core.DTOs;
using FluentValidation;

namespace Core.Validators
{
    class ProjectThreadValidator : AbstractValidator<CreateProjectThreadDto>
    {
        public ProjectThreadValidator()
        {
            RuleFor(g => g.Title).NotNull().NotEmpty();
        }
    }
}
