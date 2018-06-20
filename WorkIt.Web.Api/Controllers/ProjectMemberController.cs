using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WorkIt.Core;
using WorkIt.Web.Api.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkIt.Web.Api.Controllers
{
    [Route("api/projectmembers")]
    public class ProjectMemberController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectMemberController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]


        [HttpPost("{projectId}")]
        public async Task<IActionResult> AddMember([FromBody]ProjectMembershipDto projectMembership, long projectId)
        {
            var currentUserId = GetOpenIdSub();
            var response = await _projectService.AddMemberToProject(currentUserId, projectMembership.ProjectId, projectMembership.UserId);
            return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));
        }

        [HttpDelete("{projectId}")]
        public async Task<IActionResult> RemoveMember(ProjectMembershipDto projectMembership)
        {
            var currentUserId = GetOpenIdSub();
            var response = await _projectService.RemoveMemberFromProject(projectMembership.ProjectId, projectMembership.UserId);
            return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));
        }
    }
}
