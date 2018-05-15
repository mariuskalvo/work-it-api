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
using WorkIt.Web.Api.Utils;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
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
        public async Task<IActionResult> Get(int page = 1)
        {
            int pageSize = 10;
            var projects = await _projectService.Get(page, pageSize);

            return new OkObjectResult(projects);

        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectDto createGroupDto)
        {
            var jwtUserSubject = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserId = await _userService.GetCurrentUserId(jwtUserSubject);

            if (currentUserId == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            var response = await _projectService.Create(createGroupDto, currentUserId);

            if (response.Status != CrudStatus.Ok)
                return StatusCode(CrudStatusMapper.MapCrudStatusToStatusCode(response.Status));
            else
                return new OkObjectResult(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddMember(ProjectMembershipDto projectMembership)
        {
            var response = await _projectService.AddMemberToProject(projectMembership.ProjectId, 
                                                                    projectMembership.UserId);

            return StatusCode(CrudStatusMapper.MapCrudStatusToStatusCode(response.Status));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveMember(ProjectMembershipDto projectMembership)
        {
            var response = await _projectService.RemoveMemberFromProject(projectMembership.ProjectId,
                                                                         projectMembership.UserId);

            return StatusCode(CrudStatusMapper.MapCrudStatusToStatusCode(response.Status));
        }
    }
}