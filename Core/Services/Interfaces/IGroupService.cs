using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOS;

namespace Core.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupDto>> Get(int limit);
        Task<GroupDto> Create(CreateGroupDto createGroupDto);
    }
}
