﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Project> Groups { get; set; }
        public virtual DbSet<ProjectThread> Threads { get; set; }
        public virtual DbSet<ThreadEntry> ThreadEntries { get; set; }
        public virtual DbSet<ThreadEntryReaction> ThreadEntryReactions { get; set; }


        public AppDbContext() { }
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // tb => EntityTypeBuilder<Type>

            modelBuilder.Entity<Project>(tb => tb.HasMany(grp => grp.Threads));
            modelBuilder.Entity<Project>().HasOne(grp => grp.CreatedBy);

            modelBuilder.Entity<ProjectThread>(tb => tb.HasOne(thread => thread.Group));
            modelBuilder.Entity<ProjectThread>(tb => tb.HasOne(thread => thread.CreatedBy));
            modelBuilder.Entity<ProjectThread>(tb => tb.HasMany(thread => thread.Entries));

            modelBuilder.Entity<ThreadEntry>(tb => tb.HasOne(entry => entry.Thread));
            modelBuilder.Entity<ThreadEntry>(tb => tb.HasOne(entry => entry.CreatedBy));
            modelBuilder.Entity<ThreadEntry>(tb => tb.HasMany(entry => entry.Reactions));

            modelBuilder.Entity<ThreadEntryReaction>(tb => tb.HasOne(reaction => reaction.ThreadEntry));
            modelBuilder.Entity<ThreadEntryReaction>(tb => tb.HasOne(reaction => reaction.CreatedBy));

            modelBuilder.Entity<ApplicationUserOwnedProjects>().HasKey(a => new { a.ApplicationUserId, a.GroupId });

            modelBuilder.Entity<ApplicationUserOwnedProjects>()
                        .HasOne(a => a.Group)
                        .WithMany(grp => grp.Owners)
                        .HasForeignKey(a => a.GroupId);

            modelBuilder.Entity<ApplicationUserOwnedProjects>()
                        .HasOne(a => a.ApplicationUser)
                        .WithMany(user => user.OwnedGroups)
                        .HasForeignKey(a => a.ApplicationUserId);


        }
    }
}
