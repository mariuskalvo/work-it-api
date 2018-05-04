using System;
using System.Collections.Generic;
using System.Text;
using Core.DTOs;
using FluentValidation;

namespace Core.Validators
{
    class AccountValidator : AbstractValidator<CreateAccountDto>
    {
        public AccountValidator()
        {
            RuleFor(a => a.Password.Equals(a.RepeatPassword));
        }
    }
}
