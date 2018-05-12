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
        private readonly IProjectThreadService _projectThreadService;
        private readonly IUserService _userService;

        public ThreadController(IProjectThreadService projectThreadService, IUserService userService)
        {
            _projectThreadService = projectThreadService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ProjectThreadDto> Create(CreateProjectThreadDto thread)
        {
            var jwtUserSubject = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserId = await _userService.GetCurrentUserId(jwtUserSubject);
            return _projectThreadService.Create(thread, currentUserId);
        }

        [HttpGet]
        public IEnumerable<ProjectThreadDto> GetLatestByProjectId(int limit, long projectId)
        {
            int actualLimit = Math.Max(0, Math.Min(limit, 10));
            return _projectThreadService.GetLatestByProjectId(actualLimit, projectId);
        }

        [HttpGet]
        public IEnumerable<ProjectThreadDto> GetByGroupId(long groupId, int page = 1)
        {
            return _projectThreadService.GetPagedByProjectId(groupId, page);
        }
    }
}