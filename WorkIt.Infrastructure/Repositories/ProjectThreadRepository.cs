using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using WorkIt.Core.Interfaces.Repositories;
using WorkIt.Infrastructure.DataAccess;

namespace WorkIt.Infrastructure.Repositories
{
    public class ProjectThreadRepository : IProjectThreadRepository
    {
        private readonly AppDbContext _dbContext;

        public ProjectThreadRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectThread> Create(ProjectThread projectThread)
        {
            _dbContext.Threads.Add(projectThread);
            await _dbContext.SaveChangesAsync();
            return projectThread;
        }

        public async Task<IEnumerable<ProjectThread>> GetProjectThreads(long projectId, int take, int skip)
        {
            return await _dbContext.Threads
                         .Take(take)
                         .Skip(skip)
                         .OrderByDescending(p => p.CreatedAt)
                         .ToListAsync();
        }
    }
}
