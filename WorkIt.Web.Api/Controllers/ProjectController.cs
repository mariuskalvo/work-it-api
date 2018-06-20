using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Services;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkIt.Core;
using WorkIt.Core.Constants;
using WorkIt.Core.DTOs.Project;
using WorkIt.Web.Api.Controllers;
using WorkIt.Web.Api.Utils;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/projects")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("memberships")]
        public async Task<IActionResult> GetProjectWithMembership()
        {
            var userOpenIdSub =  GetOpenIdSub();
            var response = await _projectService.GetMemberProjectsForUser(userOpenIdSub);
            return MapActionResultWithData(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var currentUserId =  GetOpenIdSub();
            var response = await _projectService.GetProjects(currentUserId);
            return MapActionResultWithData(response);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectDetailsById(long projectId)
        {
            var currentUserId =  GetOpenIdSub();
            var response = await _projectService.GetProjectDetailsByProjectId(projectId, currentUserId);
            return MapActionResultWithData(response);
        }

        [HttpGet("detailedprojects")]
        public async Task<IActionResult> GetDetailedProjects()
        {
            var currentUserId =  GetOpenIdSub();
            var response = await _projectService.GetDetailedProjects(currentUserId);
            return MapActionResultWithData(response);
        }

        [HttpGet("recent")]
        public async Task<IActionResult> GetLastUpdatedUserProjects()
        {
            var numberOfProjects = 4;
            var currentUserId = GetOpenIdSub();
            var response = await _projectService.GetLastUpdatedProjects(currentUserId, numberOfProjects);
            return MapActionResultWithData(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateProjectDto createGroupDto)
        {
            var currentUserId =  GetOpenIdSub();
            var response = await _projectService.Create(createGroupDto, currentUserId);
            return MapActionResultWithData(response);
        }
    }
}