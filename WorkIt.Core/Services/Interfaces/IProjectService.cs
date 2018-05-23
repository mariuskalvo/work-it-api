using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOS;
using WorkIt.Core.DTOs;

namespace Core.Services
{
    public interface IProjectService
    {
        Task<CrudServiceResponse<IEnumerable<ProjectDto>>> Get(string currentUserId);
        Task<CrudServiceResponse<ProjectDto>> Create(CreateProjectDto createGroupDto, string applicationUserId);
        Task<CrudServiceResponse> AddMemberToProject(long projectId, string userId);
        Task<CrudServiceResponse> RemoveMemberFromProject(long projectId, string userId);
    }
}
