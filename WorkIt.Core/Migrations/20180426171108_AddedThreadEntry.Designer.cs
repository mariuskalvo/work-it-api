﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Core.DataAccess;
using System;

namespace Core.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180426171108_AddedThreadEntry")]
    partial class AddedThreadEntry
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Core.Entities.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Core.Entities.GroupThread", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("GroupId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("Core.Entities.ThreadEntry", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<long>("GroupThreadId");

                    b.HasKey("Id");

                    b.HasIndex("GroupThreadId");

                    b.ToTable("ThreadEntries");
                });

            modelBuilder.Entity("Core.Entities.GroupThread", b =>
                {
                    b.HasOne("Core.Entities.Group", "Group")
                        .WithMany("Threads")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Entities.ThreadEntry", b =>
                {
                    b.HasOne("Core.Entities.GroupThread", "Thread")
                        .WithMany("Entries")
                        .HasForeignKey("GroupThreadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}