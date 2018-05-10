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
        public IEnumerable<ThreadEntryDto> GetByThreadId(long threadId)
        {
            return _threadEntryService.GetByThreadId(threadId);
        }

        [HttpPost]
        public async Task<ThreadEntryDto> Create(CreateThreadEntryDto createEntry)
        {
            var jwtUserSubjectEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserId = await _userService.GetCurrentUserId(jwtUserSubjectEmail);
            var createdDto = _threadEntryService.Create(createEntry, currentUserId);
            return createdDto;
        }

        [HttpPost]
        public async Task AddReactionForThreadEntry(AddEntryReactionDto addReaction)
        {
            var jwtUserSubjectEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUserId = await _userService.GetCurrentUserId(jwtUserSubjectEmail);
            _threadEntryService.AddReactionToThreadEntry(addReaction, currentUserId);
        }
    }
}