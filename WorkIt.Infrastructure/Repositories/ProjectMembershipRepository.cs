using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkIt.Core.Entities;
using WorkIt.Core.Services.Interfaces;
using WorkIt.Infrastructure.DataAccess;

namespace WorkIt.Infrastructure.Repositories
{
    public class ProjectMembershipRepository : IProjectMembershipRepository
    {
        private readonly AppDbContext _dbContext;
        public ProjectMembershipRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddMemberToProject(string userId, long projectId)
        {
            _dbContext.ProjectMembers.Add(new ApplicationUserProjectMember()
            {
                ApplicationUserId = userId,
                ProjectId = projectId
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveMembership(ApplicationUserProjectMember membership)
        {
            _dbContext.Remove(membership);
            await _dbContext.SaveChangesAsync();
        }

        public Task<ApplicationUserProjectMember> GetProjectMembership(long projectId, string userId)
        {
            return _dbContext.ProjectMembers.FindAsync(userId, projectId);
        }

        public async Task<IEnumerable<ApplicationUserProjectMember>> GetProjectMemberships(string userId)
        {
            return await _dbContext.ProjectMembers.Where(pm => pm.ApplicationUserId == userId)
                                                  .Include(pm => pm.Project)
                                                  .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUserProjectMember>> GetProjectMembershipsByUserId(string userId)
        {
            return await _dbContext.ProjectMembers.Where(pm => pm.ApplicationUserId == userId)
                                      .Include(pm => pm.ApplicationUser)
                                      .ToListAsync();
        }
    }
}
