using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.RepositoryInterfaces;
using Persistence.Database;

namespace Persistence.Repositories
{
    public class GroupThreadRepository : IGroupThreadRepository
    {
        private readonly AppDbContext context;

        public GroupThreadRepository(AppDbContext appDbContext)
        {
            this.context = appDbContext;
        }

        public async Task<bool> Add(GroupThread groupThread)
        {
            context.Threads.Add(groupThread);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
