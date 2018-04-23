using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ThreadController : Controller
    {
        private readonly IGroupThreadService groupThreadService;

        public ThreadController(IGroupThreadService groupThreadService)
        {
            this.groupThreadService = groupThreadService;
        }

        [HttpPost]
        public async Task<GroupThreadDto> Create(CreateGroupThreadDto thread)
        {
            return await groupThreadService.Create(thread);
        }
    }
}