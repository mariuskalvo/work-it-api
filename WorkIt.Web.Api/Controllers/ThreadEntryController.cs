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
using WorkIt.Web.Api.Utils;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class ThreadEntryController : Controller
    {
        private readonly IThreadEntryService _threadEntryService;
        private readonly IUserService _userService;

        public ThreadEntryController(IThreadEntryService threadEntryService, IUserService userService)
        {
            _threadEntryService = threadEntryService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByThreadId(long threadId, int page = 1)
        {
            int pageSize = 10;
            var response = await _threadEntryService.GetPagedByThreadId(threadId, page, pageSize);

            if (response.Status != ServiceStatus.Ok)
                return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));

            return new OkObjectResult(response.Data);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateThreadEntryDto createEntry)
        {
            var jwtUserSubjectEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserId = await _userService.GetCurrentUserId(jwtUserSubjectEmail);
            var response = await _threadEntryService.Create(createEntry, currentUserId);

            if (response.Status != ServiceStatus.Ok)
                return StatusCode(ServiceStatusMapper.MapToHttpStatusCode(response.Status));

            return new OkObjectResult(response.Data);
        }
    }
}