using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupThread> Threads { get; set; }
        public virtual DbSet<ThreadEntry> ThreadEntries { get; set; }
        public virtual DbSet<ThreadEntryReaction> ThreadEntryReactions { get; set; }


        public AppDbContext() { }
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // tb => EntityTypeBuilder<Type>

            modelBuilder.Entity<Group>(tb => tb.HasMany(grp => grp.Threads));
            modelBuilder.Entity<GroupThread>(tb => tb.HasOne(thread => thread.Group));

            modelBuilder.Entity<GroupThread>(tb => tb.HasMany(thread => thread.Entries));
            modelBuilder.Entity<ThreadEntry>(tb => tb.HasOne(entry => entry.Thread));
        }
    }
}
