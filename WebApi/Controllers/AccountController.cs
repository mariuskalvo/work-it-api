using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<ValidationResponse> CreateAccount(CreateAccountDto createAccountDto)
        {
            var validationResult = await accountService.CreateAccount(createAccountDto);
            return validationResult;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await accountService.IssueToken(loginDto);
            if (String.IsNullOrEmpty(token))
            {
                return StatusCode(StatusCodes.Status401Unauthorized);
            }
            return Ok(token);
        }
    }
}
