using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
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
            modelBuilder.Entity<Group>().HasOne(grp => grp.CreatedBy);

            modelBuilder.Entity<GroupThread>(tb => tb.HasOne(thread => thread.Group));
            modelBuilder.Entity<GroupThread>(tb => tb.HasOne(thread => thread.CreatedBy));

            modelBuilder.Entity<GroupThread>(tb => tb.HasMany(thread => thread.Entries));
            modelBuilder.Entity<ThreadEntry>(tb => tb.HasOne(entry => entry.Thread));

            modelBuilder.Entity<ThreadEntry>(tb => tb.HasMany(entry => entry.Reactions));
            modelBuilder.Entity<ThreadEntryReaction>(tb => tb.HasOne(reaction => reaction.ThreadEntry));

            modelBuilder.Entity<ApplicationUserOwnedGroups>().HasKey(a => new { a.ApplicationUserId, a.GroupId });

            modelBuilder.Entity<ApplicationUserOwnedGroups>()
                        .HasOne(a => a.Group)
                        .WithMany(grp => grp.Owners)
                        .HasForeignKey(a => a.GroupId);

            modelBuilder.Entity<ApplicationUserOwnedGroups>()
                        .HasOne(a => a.ApplicationUser)
                        .WithMany(user => user.OwnedGroups)
                        .HasForeignKey(a => a.ApplicationUserId);


        }
    }
}
