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
    [Route("api/projects")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService, IUserService userService) : base(userService)
        {
            _projectService = projectService;
        }

        [HttpGet("memberships")]
        public async Task<IActionResult> GetProjectWithMembership()
        {
            var currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var response = await _projectService.GetMemberProjectsForUser(currentUserId);
            return MapActionResultWithData(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var response = await _projectService.GetProjects(currentUserId);
            return MapActionResultWithData(response);
        }

        [HttpGet("detailedprojects")]
        public async Task<IActionResult> GetDetailedProjects()
        {
            var currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var response = await _projectService.GetDetailedProjects(currentUserId);
            return MapActionResultWithData(response);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetLastUpdatedUserProjects()
        {
            var currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var numberOfProjects = 4;
            var response = await _projectService.GetLastUpdatedProjects(currentUserId, numberOfProjects);

            return MapActionResultWithData(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjectDto createGroupDto)
        {
            var currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var response = await _projectService.Create(createGroupDto, currentUserId);
            return MapActionResultWithData(response);
        }

        [HttpPost("{projectId}/members")]
        public async Task<IActionResult> AddMember(long projectId, [FromBody] ProjectMembershipDto projectMembership)
        {
            var currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var response = await _projectService.AddMemberToProject(currentUserId, projectMembership.ProjectId, projectMembership.UserId);
            return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));
        }

        [HttpDelete("{projectId}/members")]
        public async Task<IActionResult> RemoveMember(ProjectMembershipDto projectMembership)
        {
            var currentUserId = await GetCurrentUserIdAsync();
            if (currentUserId == null)
                return StatusCode(StatusCodes.Status401Unauthorized);

            var response = await _projectService.RemoveMemberFromProject(projectMembership.ProjectId,projectMembership.UserId);
            return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));
        }
    }
}