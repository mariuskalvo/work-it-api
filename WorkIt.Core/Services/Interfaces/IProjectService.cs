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
        Task<ServiceResponse<IEnumerable<ProjectDto>>> GetMemberProjectsForUser(string currentUserId);
        Task<ServiceResponse<IEnumerable<ProjectDto>>> GetProjects(string currentUserId);
        Task<ServiceResponse<IEnumerable<ProjectDto>>> GetLastUpdatedProjects(string currentUserId, int limit);

        Task<ServiceResponse<ProjectDto>> Create(CreateProjectDto createGroupDto, string applicationUserId);
        Task<ServiceResponse> AddMemberToProject(long projectId, string userId);
        Task<ServiceResponse> RemoveMemberFromProject(long projectId, string userId);
    }
}
