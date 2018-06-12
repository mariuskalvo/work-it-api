using System;
using System.Collections;
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

        public async Task AddMemberToProject(long userId, long projectId)
        {
            _dbContext.ProjectMembers.Add(new ApplicationUserProjectMember()
            {
                UserInfoId = userId,
                ProjectId = projectId
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveMembership(ApplicationUserProjectMember membership)
        {
            _dbContext.Remove(membership);
            await _dbContext.SaveChangesAsync();
        }

        public Task<ApplicationUserProjectMember> GetProjectMembership(long projectId, long userId)
        {
            return _dbContext.ProjectMembers.FindAsync(userId, projectId);
        }

        public async Task<IEnumerable<ApplicationUserProjectMember>> GetProjectMembershipsByUserId(string userId)
        {
            return await _dbContext.ProjectMembers.Where(pm => pm.UserInfo.OpenIdSub == userId)
                                      .Include(pm => pm.UserInfo)
                                      .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUserProjectMember>> GetProjectMembershipsByProjectId(long projectId)
        {
            return await _dbContext.ProjectMembers.Where(pm => pm.ProjectId == projectId)
                                                  .Include(pm => pm.UserInfo)
                                                  .ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUserOwnedProjects>> GetProjectOwnersByProjectId(long projectId)
        {
            return await _dbContext.ProjectOwners.Where(po => po.ProjectId == projectId)
                                    .Include(po => po.UserInfo)
                                    .ToListAsync();
        }
    }
}
