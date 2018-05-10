using System;
using System.Collections.Generic;
using System.Text;
using Core.DTOs;
using FluentValidation;

namespace Core.Validators
{
    class GroupThreadValidator : AbstractValidator<CreateGroupThreadDto>
    {
        public GroupThreadValidator()
        {
            RuleFor(g => g.Title).NotNull().NotEmpty();
        }
    }
}
