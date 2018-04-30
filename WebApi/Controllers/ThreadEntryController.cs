using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThreadEntryController : Controller
    {
        private readonly IThreadEntryService threadEntryService;

        public ThreadEntryController(IThreadEntryService threadEntryService)
        {
            this.threadEntryService = threadEntryService;
        }

        [HttpGet]
        public IEnumerable<ThreadEntryDto> GetByThreadId(long threadId)
        {
            return threadEntryService.GetByThreadId(threadId);
        }

        [HttpPost]
        public ThreadEntryDto Create(CreateThreadEntryDto createEntry)
        {
            var createdDto = threadEntryService.Create(createEntry);
            return createdDto;
        }

    }
}