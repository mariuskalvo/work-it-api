using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.Services.Interfaces;
using Core.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<CrudServiceResponse> CreateAccount(CreateAccountDto createAccount)
        {
            var createAccountValidator = new AccountValidator();
            var validationResult = createAccountValidator.Validate(createAccount);

            if (!validationResult.IsValid)
            {
                var validationMessage = String.Join(". ", validationResult.Errors);
                return new CrudServiceResponse(CrudStatus.BadRequest, validationMessage);
            }

            var userToAdd = new ApplicationUser()
            {
                Email = createAccount.Email,
                UserName = createAccount.Email
            };

            var createResult = await userManager.CreateAsync(userToAdd, createAccount.Password);

            if (!createResult.Succeeded)
                return new CrudServiceResponse(CrudStatus.Error, "Could not create user account");

            return new CrudServiceResponse(CrudStatus.Ok);
        }

        public async Task<CrudServiceResponse<string>> IssueToken(LoginDto loginDto)
        {
            var signInResult = await signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
            if (signInResult.Succeeded)
            {
                var currentUser = userManager.Users.FirstOrDefault(user => 
                    user.UserName.Equals(loginDto.Email, StringComparison.InvariantCultureIgnoreCase));
                var token = GenerateJwtToken(currentUser);

                return new CrudServiceResponse<string>(CrudStatus.Ok).SetData(token);
            }
            return new CrudServiceResponse<string>(CrudStatus.Unauthorized);
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretsecretsecretsecretsecret"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1);
            var token = new JwtSecurityToken(
                "http://localhost:55437",
                "http://localhost:55437",
                userClaims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
