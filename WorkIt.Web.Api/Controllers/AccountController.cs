using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkIt.Core.Constants;
using WorkIt.Web.Api.Utils;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountDto createAccountDto)
        {
            var response = await _accountService.CreateAccount(createAccountDto);
            return StatusCode(CrudStatusMapper.MapCrudStatusToStatusCode(response.Status));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _accountService.IssueToken(loginDto);
            if (response.Status != CrudStatus.Ok)
                return StatusCode(CrudStatusMapper.MapCrudStatusToStatusCode(response.Status));

            return new OkObjectResult(response.Data);
        }
    }
}
