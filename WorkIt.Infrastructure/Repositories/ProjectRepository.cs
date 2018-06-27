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
                 join user in _dbContext.UserInfos on projectMember.UserInfoId equals user.Id
                 where user.OpenIdSub == currentUserId
                 orderby project.ModifiedAt descending
                 select project
                )
                .Take(limit)
                .ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<Project>> GetMemberProjectsForUser(string userId)
        {
            var entities = await 
                (from project in _dbContext.Projects
                join projectMember in _dbContext.ProjectMembers
                on project.Id equals projectMember.ProjectId
                 join user in _dbContext.UserInfos on projectMember.UserInfoId equals user.Id
                 where user.OpenIdSub == userId
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
    }
}
