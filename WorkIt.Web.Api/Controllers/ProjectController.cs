using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOS;
using Core.Services;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IUserService _userService;

        public ProjectController(IProjectService projectService, IUserService userService)
        {
            _projectService = projectService;
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<ProjectDto> Get()
        {
            return _projectService.Get(10);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto createGroupDto)
        {
            var jwtUserSubject = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserId = await _userService.GetCurrentUserId(jwtUserSubject);

            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var createProjectDto = _projectService.Create(createGroupDto, currentUserId);

            return Ok(createProjectDto);
        }
    }
}