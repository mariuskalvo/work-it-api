using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories
{
    class ThreadEntryRepoistory : IThreadEntryRepository
    {
        private readonly AppDbContext context;

        public ThreadEntryRepoistory(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ThreadEntry> Create(ThreadEntry threadEntry)
        {
            context.ThreadEntries.Add(threadEntry);
            await context.SaveChangesAsync();
            return threadEntry;
        }

        public async Task<IEnumerable<ThreadEntry>> GetByThreadId(long threadId)
        {
            return await context.ThreadEntries.Where(t => t.GroupThreadId == threadId).ToListAsync();
        }
    }
}
