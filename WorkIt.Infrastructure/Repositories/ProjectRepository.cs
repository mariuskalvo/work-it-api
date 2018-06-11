using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkIt.Core.Entities;
using WorkIt.Core.Interfaces.Repositories;
using WorkIt.Infrastructure.DataAccess;

namespace WorkIt.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _dbContext;

        public ProjectRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> Create(Project project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();
            return project;
        }

        public async Task<Project> GetById(long id)
        {
            return await _dbContext.Projects.FindAsync(id);
        }

        public async Task<IEnumerable<Project>> GetLastUpdatedUserProjects(string currentUserId, int limit)
        {
            var entities = await
                (from project in _dbContext.Projects
                 join projectMember in _dbContext.ProjectMembers
                 on project.Id equals projectMember.ProjectId
                 where projectMember.ApplicationUserId == currentUserId
                 orderby project.ModifiedAt descending
                 select project
                )
                .Take(limit)
                .ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<Project>> GetMemberProjectsForUser(string currentUserId)
        {
            var entities = await 
                (from project in _dbContext.Projects
                join projectMember in _dbContext.ProjectMembers
                on project.Id equals projectMember.ProjectId
                where projectMember.ApplicationUserId == currentUserId
                orderby project.CreatedAt descending
                select project
                ).ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetDetailedProjects()
        {
            return await _dbContext
                .Projects
                .Include(p => p.CreatedBy)
                .ToListAsync();
        }

        public async Task<ApplicationUserOwnedProjects> GetProjectsOwnership(string currentUserId, long projectId)
        {
            return await _dbContext.ProjectOwners
                            .Where(po => po.ApplicationUserId == currentUserId && po.ProjectId == projectId)
                            .FirstOrDefaultAsync();
        }
    }
}
