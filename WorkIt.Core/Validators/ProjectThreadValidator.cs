using System;
using System.Collections.Generic;
using System.Text;
using Core.DTOs;
using FluentValidation;
using WorkIt.Core.DTOs.ProjectThread;

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
