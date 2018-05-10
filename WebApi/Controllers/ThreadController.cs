using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ThreadController : Controller
    {
        private readonly IGroupThreadService _groupThreadService;
        private readonly IUserService _userService;

        public ThreadController(IGroupThreadService groupThreadService, IUserService userService)
        {
            _groupThreadService = groupThreadService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<GroupThreadDto> Create(CreateGroupThreadDto thread)
        {
            var jwtUserSubject = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userService.GetCurrentUser(jwtUserSubject);

            return _groupThreadService.Create(thread, currentUser.Id);
        }

        [HttpGet]
        public IEnumerable<GroupThreadDto> GetLatest(int limit)
        {
            return _groupThreadService.GetLatest(limit);
        }

        [HttpGet]
        public IEnumerable<GroupThreadDto> GetByGroupId(long groupId, int page = 1)
        {
            return _groupThreadService.GetPagedByGroupId(groupId, page);
        }
    }
}