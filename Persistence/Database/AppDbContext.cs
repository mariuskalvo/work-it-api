using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupThread> Threads { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // tb => EntityTypeBuilder<Type>

            modelBuilder.Entity<Group>(tb => tb.HasMany(thread => thread.Threads));
            modelBuilder.Entity<GroupThread>(tb => tb.HasOne(grp => grp.Group));
        }
    }
}
