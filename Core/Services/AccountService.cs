using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Services.Interfaces;
using Core.Validators;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ValidationResponse> CreateAccount(CreateAccountDto createAccount)
        {
            var createAccountValidator = new AccountValidator();
            var validationResult = createAccountValidator.Validate(createAccount);

            if (!validationResult.IsValid)
            {
                return new ValidationResponse()
                {
                    Success = false,
                    Errors = validationResult.Errors.Select(err => err.ErrorMessage)
                };
            }

            var userToAdd = new ApplicationUser()
            {
                Email = createAccount.Email,
                UserName = createAccount.Email
            };

            var createResult = await userManager.CreateAsync(userToAdd, createAccount.Password);
            if (!createResult.Succeeded)
            {
                return new ValidationResponse()
                {
                    Success = false,
                    Errors = createResult.Errors.Select(err => err.Description)
                };
            }

            return new ValidationResponse()
            {
                Success = true
            };
        }
    }
}
