using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkIt.Core.Entities;

namespace WorkIt.Core.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> Create(Project project);
        Task<Project> GetById(long id);
        Task<IEnumerable<Project>> GetMemberProjectsForUser(string userId);
        Task<IEnumerable<Project>> GetLastUpdatedUserProjects(string userId, int limit);
        Task<IEnumerable<Project>> GetProjects();
        Task<IEnumerable<Project>> GetDetailedProjects();
        Task<ApplicationUserOwnedProjects> GetProjectsOwnership(string userId, long projectId);
    }
}
