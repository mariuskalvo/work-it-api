using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Entities;
using Core.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories
{
    public class GroupThreadRepository : IGroupThreadRepository
    {
        private readonly AppDbContext context;

        public GroupThreadRepository(AppDbContext appDbContext)
        {
            context = appDbContext;
        }

        public async Task<bool> Add(GroupThread groupThread)
        {
            context.Threads.Add(groupThread);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<GroupThread>> GetLatest(int limit)
        {
            return await context.Threads.OrderByDescending(g => g.CreatedAt).Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<GroupThread>> GetByGroupId(long groupId)
        {
            return await context.Threads.Where(t => t.GroupId == groupId).ToListAsync();
        }

        public async Task<IEnumerable<GroupThread>> GetByGroupIdWithSkipAndLimit(long groupId, int take, int skip)
        {
            return await context.Threads.Where(t => t.GroupId == groupId)
                                .Skip(skip)
                                .Take(take)
                                .ToListAsync();
        }
    }
}
