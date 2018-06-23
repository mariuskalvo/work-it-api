using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkIt.Core.Entities;

namespace WorkIt.Core.Interfaces.Repositories
{
    public interface IProjectThreadRepository
    {
        Task<ProjectThread> Create(ProjectThread projectThread);
        Task<IEnumerable<ProjectThread>> GetProjectThreads(long projectId, int take, int skip);
        Task<ProjectThread> GetById(long threadId);

    }
}
