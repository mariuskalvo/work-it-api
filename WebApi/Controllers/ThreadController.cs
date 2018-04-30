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
    public class ThreadController : Controller
    {
        private readonly IGroupThreadService groupThreadService;

        public ThreadController(IGroupThreadService groupThreadService)
        {
            this.groupThreadService = groupThreadService;
        }

        [HttpPost]
        public GroupThreadDto Create(CreateGroupThreadDto thread)
        {
            return groupThreadService.Create(thread);
        }

        [HttpGet]
        public IEnumerable<GroupThreadDto> GetLatest(int limit)
        {
            return groupThreadService.GetLatest(limit);
        }

        [HttpGet]
        public IEnumerable<GroupThreadDto> GetByGroupId(long groupId, int page = 1)
        {
            return groupThreadService.GetPagedByGroupId(groupId, page);
        }
    }
}