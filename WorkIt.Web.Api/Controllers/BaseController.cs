using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WorkIt.Web.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserService _userService;

        public BaseController(IUserService userService)
        {
            _userService = userService;
        }

        protected async Task<string> GetCurrentUserIdAsync()
        {
            var jwtUserSubject = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserId = await _userService.GetCurrentUserId(jwtUserSubject);
            return currentUserId;
        }
    }
}