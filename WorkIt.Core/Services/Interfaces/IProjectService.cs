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
        Task<ServiceResponse<IEnumerable<RecentlyUpdatedProjectDto>>> GetLastUpdatedProjects(string currentUserId, int limit);
        Task<ServiceResponse<IEnumerable<DetailedProjectListEntryDto>>> GetDetailedProjects(string currentUserId);
        Task<ServiceResponse<ProjectDto>> Create(CreateProjectDto createGroupDto, string applicationUserId);
        Task<ServiceResponse> AddMemberToProject(string currentUserId, long projectId, string userIdToBeAdded);
        Task<ServiceResponse> RemoveMemberFromProject(long projectId, string userId);
    }
}
