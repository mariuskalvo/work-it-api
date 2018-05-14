using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOS;

namespace Core.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> Get(int page, int pageSize);
        Task<ProjectDto> Create(CreateProjectDto createGroupDto, string applicationUserId);
        Task AddMemberToProject(long projectId, string userId);
        Task RemoveMemberFromProject(long projectId, string userId);
    }
}
