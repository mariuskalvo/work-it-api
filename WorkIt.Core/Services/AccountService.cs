using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Services.Interfaces;
using Core.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs;
using WorkIt.Core.DTOs.Auth0;
using WorkIt.Core.Entities;
using WorkIt.Core.Interfaces.Repositories;

namespace Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInfoRepository _userInfoRepository;

        public AccountService(IConfiguration configuration, IUserInfoRepository userInfoRepository)
        {
            _configuration = configuration;
            _userInfoRepository = userInfoRepository;
        }

        public async Task<ServiceResponse<string>> Login(LoginDto loginDto)
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
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _configuration["TokenEndpoint"]);
                httpRequestMessage.Content = new StringContent(requestJson, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(httpRequestMessage);
                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<Auth0LoginResponse>(responseAsString);

                    var jwtToken = new JwtSecurityToken(loginResponse.AccessToken);
                    var subject = jwtToken.Subject;
                    
                    if (!string.IsNullOrEmpty(subject))
                    {
                        var existingUserInfo = await _userInfoRepository.GetUserInfoByOpenIdSub(subject);
                        if (existingUserInfo == null)
                        {
                            await _userInfoRepository.CreateDefaultUserInfo(subject);
                        }
                    }
                    return new ServiceResponse<string>(ServiceStatus.Ok).SetData(loginResponse.AccessToken);
                }
                return new ServiceResponse<string>(ServiceStatus.BadRequest);
            }
        }
    }
}
