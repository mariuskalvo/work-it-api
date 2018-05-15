using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
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

        public async Task<IEnumerable<Project>> GetProjects(int take, int skip)
        {
            var allEntities = await _dbContext.Projects.ToListAsync();
            var entities = await _dbContext.Projects
                                     .Skip(skip)
                                     .Take(take)
                                     .OrderByDescending(p => p.CreatedAt)
                                     .ToListAsync();
            return entities;
        }

        public async Task<IEnumerable<Project>> GetProjectsWithUserMembership(string userId)
        {
            var projects = await (
                 from project in _dbContext.Projects
                 join projectMember in _dbContext.ProjectMembers
                 on project.Id equals projectMember.ProjectId
                 where projectMember.ApplicationUserId == userId
                 select project
            ).ToListAsync();

            return projects;
        }


    }
}
