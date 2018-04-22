using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOS;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        public async Task<IEnumerable<GroupDto>> Get()
        {
            return await groupService.Get(10);
        }
        
        [HttpPost]
        public async Task<GroupDto> Create(CreateGroupDto createGroupDto)
        {
            return await groupService.Create(createGroupDto);
        }
    }
}