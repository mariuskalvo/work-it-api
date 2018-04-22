using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AppDbContext context;

        public GroupRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Group> Add(Group group)
        {
            context.Groups.Add(group);
            await context.SaveChangesAsync();
            return group;
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            return await context.Groups.ToListAsync();
        }

    }
}
