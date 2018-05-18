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
    public class ThreadEntryRepository : IThreadEntryRepository
    {
        private readonly AppDbContext _dbContext;

        public ThreadEntryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ThreadEntry> Create(ThreadEntry threadEntry)
        {
            _dbContext.ThreadEntries.Add(threadEntry);
            await _dbContext.SaveChangesAsync();
            return threadEntry;
        }

        public async Task<IEnumerable<ThreadEntry>> GetByThreadId(long threadId, int take, int skip)
        {
            return await _dbContext.ThreadEntries
                             .Where(te => te.GroupThreadId == threadId)        
                             .Take(take)
                             .Skip(skip)
                             .OrderByDescending(p => p.CreatedAt)
                             .ToListAsync();
        }
    }
}
