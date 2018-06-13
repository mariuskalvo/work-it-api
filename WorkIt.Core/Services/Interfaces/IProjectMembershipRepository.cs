using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkIt.Core.Entities;

namespace WorkIt.Core.Services.Interfaces
{
    public interface IProjectMembershipRepository
    {
        Task<ApplicationUserProjectMember> GetProjectMembership(long projectId, long userId);
        Task<IEnumerable<ApplicationUserProjectMember>> GetProjectMembershipsByUserId(string userId);
        Task AddMemberToProject(long userId, long projectId);
        Task RemoveMembership(ApplicationUserProjectMember membership);
    }
}
