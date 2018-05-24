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
using WorkIt.Core;
using WorkIt.Core.Constants;
using WorkIt.Web.Api.Controllers;
using WorkIt.Web.Api.Utils;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService, IUserService userService) : base(userService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var response = await _projectService.Get(currentUserId);

            if (response.Status != ServiceStatus.Ok)
                return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));

            return new OkObjectResult(response.Data);

        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto createGroupDto)
        {
            var currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var response = await _projectService.Create(createGroupDto, currentUserId);

            if (response.Status != ServiceStatus.Ok)
                return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));

            return new OkObjectResult(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember([FromBody] ProjectMembershipDto projectMembership)
        {
            var response = await _projectService.AddMemberToProject(projectMembership.ProjectId, 
                                                                    projectMembership.UserId);

            return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveMember(ProjectMembershipDto projectMembership)
        {
            var response = await _projectService.RemoveMemberFromProject(projectMembership.ProjectId,
                                                                         projectMembership.UserId);

            return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));
        }
    }
}