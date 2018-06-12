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
using WorkIt.Web.Api.Models;
using WorkIt.Web.Api.Utils;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            using (var httpClient = new HttpClient())
            {
                var request = new Auth0LoginRequest()
                {
                    ClientId = _configuration["Auth0:ClientId"],
                    ClientSecret = _configuration["Auth0:ClientSecret"],
                    Audience = _configuration["Auth0:Audience"],
                    GrantType = "password",
                    Email = loginDto.Email,
                    Password = loginDto.Password,
                    Scope = _configuration["Auth0:Scope"]
                };

                var requestJson = JsonConvert.SerializeObject(request);
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://work-it-app.eu.auth0.com/oauth/token");
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(httpRequestMessage);
                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<Auth0LoginResponse>(responseAsString);
                    return new OkObjectResult(loginResponse.AccessToken);
                } else
                {
                    string content = await response.Content.ReadAsStringAsync();
                }
            }
            return new UnauthorizedResult();
        }
    }
}
