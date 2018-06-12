using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkIt.Core.DTOs;
using WorkIt.Core.DTOs.Project;

namespace Core.Services
{
    public interface IProjectService
    {
        Task<ServiceResponse<IEnumerable<ProjectDto>>> GetMemberProjectsForUser(string currentUserOpenIdSub);
        Task<ServiceResponse<IEnumerable<ProjectDto>>> GetProjects(string currentUserOpenIdSub);
        Task<ServiceResponse<ProjectDetailsDto>> GetProjectDetailsByProjectId(long projectId, string currentUserOpenIdSub);
        Task<ServiceResponse<IEnumerable<RecentlyUpdatedProjectDto>>> GetLastUpdatedProjects(string currentUserOpenIdSub, int limit);
        Task<ServiceResponse<IEnumerable<DetailedProjectListEntryDto>>> GetDetailedProjects(string currentUserOpenIdSub);
        Task<ServiceResponse<ProjectDto>> Create(CreateProjectDto createGroupDto, string currentUserOpenIdSub);
        Task<ServiceResponse> AddMemberToProject(string currentUserOpenIdSub, long projectId, long userIdToBeAdded);
        Task<ServiceResponse> RemoveMemberFromProject(long projectId, long userId);
    }
}
