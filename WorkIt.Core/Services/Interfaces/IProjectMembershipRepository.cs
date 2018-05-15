﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkIt.Core.Entities;

namespace WorkIt.Core.Services.Interfaces
{
    public interface IProjectMembershipRepository
    {
        Task<ApplicationUserProjectMember> GetProjectMembership(long projectId, string userId);
        Task<IEnumerable<ApplicationUserProjectMember>> GetProjectMembershipsByUserId(string userId);
        Task<IEnumerable<ApplicationUserProjectMember>> GetProjectMemberships(string userId);
        Task AddMemberToProject(string userId, long projectId);
        Task RemoveMembership(ApplicationUserProjectMember membership);
    }
}