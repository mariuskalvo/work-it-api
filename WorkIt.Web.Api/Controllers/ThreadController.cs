using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkIt.Core.Constants;
using WorkIt.Web.Api.Controllers;
using WorkIt.Web.Api.Utils;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ThreadController : BaseController
    {
        private readonly IProjectThreadService _projectThreadService;
        private readonly IUserService _userService;

        public ThreadController(IProjectThreadService projectThreadService, IUserService userService)
        {
            _projectThreadService = projectThreadService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectThreadDto thread)
        {
            var userOpenIdSub = GetOpenIdSub();

            var response = await _projectThreadService.Create(thread, userOpenIdSub);

            if (response.Status != ServiceStatus.Ok)
                return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));

            return new OkObjectResult(response.Data);

        }

        [HttpGet]
        public async Task<IActionResult> GetByProjectId(long projectId, int page = 1)
        {
            int pageSize = 10;
            var userOpenIdSub = GetOpenIdSub();
            var response = await _projectThreadService.GetPagedByProjectId(projectId, page, pageSize);

            if (response.Status != ServiceStatus.Ok)
                return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));

            return new OkObjectResult(response.Data);
        }
    }
}