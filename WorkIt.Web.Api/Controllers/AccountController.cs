using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WorkIt.Core.Constants;
using WorkIt.Web.Api.Controllers;
using WorkIt.Web.Api.Models;
using WorkIt.Web.Api.Utils;

namespace WebApi.Controllers
{
    [Route("api")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var response = await _accountService.Login(loginDto);
            return MapActionResultWithData(response);
        }
    }
}
