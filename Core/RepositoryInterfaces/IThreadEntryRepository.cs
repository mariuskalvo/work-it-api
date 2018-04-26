using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.RepositoryInterfaces
{
    public interface IThreadEntryRepository
    {
        Task<ThreadEntry> Create(ThreadEntry threadEntry);
        Task<IEnumerable<ThreadEntry>> GetByThreadId(long threadId);
    }
}
