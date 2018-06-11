using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkIt.Core.Entities;

namespace WorkIt.Infrastructure.DataAccess
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectThread> Threads { get; set; }
        public virtual DbSet<ThreadEntry> ThreadEntries { get; set; }
        public virtual DbSet<ThreadEntryReaction> ThreadEntryReactions { get; set; }

        public virtual DbSet<ApplicationUserProjectMember> ProjectMembers { get; set; }
        public virtual DbSet<ApplicationUserOwnedProjects> ProjectOwners { get; set; }



        public AppDbContext() { }
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // tb => EntityTypeBuilder<Type>

            modelBuilder.Entity<Project>(tb => tb.HasMany(grp => grp.Threads));
            modelBuilder.Entity<Project>().HasOne(grp => grp.CreatedBy);

            modelBuilder.Entity<ProjectThread>(tb => tb.HasOne(thread => thread.Project));
            modelBuilder.Entity<ProjectThread>(tb => tb.HasOne(thread => thread.CreatedBy));
            modelBuilder.Entity<ProjectThread>(tb => tb.HasMany(thread => thread.Entries));

            modelBuilder.Entity<ThreadEntry>(tb => tb.HasOne(entry => entry.Thread));
            modelBuilder.Entity<ThreadEntry>(tb => tb.HasOne(entry => entry.CreatedBy));
            modelBuilder.Entity<ThreadEntry>(tb => tb.HasMany(entry => entry.Reactions));

            modelBuilder.Entity<ThreadEntryReaction>(tb => tb.HasOne(reaction => reaction.ThreadEntry));
            modelBuilder.Entity<ThreadEntryReaction>(tb => tb.HasOne(reaction => reaction.CreatedBy));

            modelBuilder.Entity<ApplicationUserOwnedProjects>().HasKey(a => new { a.ApplicationUserId, a.ProjectId });
            modelBuilder.Entity<ApplicationUserProjectMember>().HasKey(a => new { a.ApplicationUserId, a.ProjectId });

            modelBuilder.Entity<ApplicationUserOwnedProjects>()
                        .HasOne(a => a.Project)
                        .WithMany(grp => grp.Owners)
                        .HasForeignKey(a => a.ProjectId);

            modelBuilder.Entity<ApplicationUserOwnedProjects>()
                        .HasOne(a => a.ApplicationUser)
                        .WithMany(user => user.OwnedProjects)
                        .HasForeignKey(a => a.ApplicationUserId);

            modelBuilder.Entity<ApplicationUserProjectMember>()
                        .HasOne(a => a.ApplicationUser)
                        .WithMany(user => user.MemberProjects)
                        .HasForeignKey(a => a.ApplicationUserId);

            modelBuilder.Entity<ApplicationUserProjectMember>()
                        .HasOne(a => a.Project)
                        .WithMany(project => project.Members)
                        .HasForeignKey(a => a.ProjectId);


        }
    }
}
