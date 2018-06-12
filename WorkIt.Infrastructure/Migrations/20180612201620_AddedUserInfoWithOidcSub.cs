using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WorkIt.Infrastructure.Migrations
{
    public partial class AddedUserInfoWithOidcSub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_ApplicationUser_ApplicationUserId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOwners_ApplicationUser_ApplicationUserId",
                table: "ProjectOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ApplicationUser_CreatedById",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadEntries_ApplicationUser_CreatedById",
                table: "ThreadEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadEntryReactions_ApplicationUser_CreatedById",
                table: "ThreadEntryReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_ApplicationUser_CreatedById",
                table: "Threads");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_Threads_CreatedById",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_ThreadEntryReactions_CreatedById",
                table: "ThreadEntryReactions");

            migrationBuilder.DropIndex(
                name: "IX_ThreadEntries_CreatedById",
                table: "ThreadEntries");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectOwners",
                table: "ProjectOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMembers",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ProjectOwners");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "ProjectMembers");

            migrationBuilder.AddColumn<long>(
                name: "CreatedById1",
                table: "Threads",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedById1",
                table: "ThreadEntryReactions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedById1",
                table: "ThreadEntries",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatedById1",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserInfoId",
                table: "ProjectOwners",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "UserInfoId",
                table: "ProjectMembers",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectOwners",
                table: "ProjectOwners",
                columns: new[] { "UserInfoId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMembers",
                table: "ProjectMembers",
                columns: new[] { "UserInfoId", "ProjectId" });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OpenIdSub = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Threads_CreatedById1",
                table: "Threads",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadEntryReactions_CreatedById1",
                table: "ThreadEntryReactions",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadEntries_CreatedById1",
                table: "ThreadEntries",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById1",
                table: "Projects",
                column: "CreatedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_UserInfos_UserInfoId",
                table: "ProjectMembers",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOwners_UserInfos_UserInfoId",
                table: "ProjectOwners",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_UserInfos_CreatedById1",
                table: "Projects",
                column: "CreatedById1",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadEntries_UserInfos_CreatedById1",
                table: "ThreadEntries",
                column: "CreatedById1",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadEntryReactions_UserInfos_CreatedById1",
                table: "ThreadEntryReactions",
                column: "CreatedById1",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_UserInfos_CreatedById1",
                table: "Threads",
                column: "CreatedById1",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectMembers_UserInfos_UserInfoId",
                table: "ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOwners_UserInfos_UserInfoId",
                table: "ProjectOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_UserInfos_CreatedById1",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadEntries_UserInfos_CreatedById1",
                table: "ThreadEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_ThreadEntryReactions_UserInfos_CreatedById1",
                table: "ThreadEntryReactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_UserInfos_CreatedById1",
                table: "Threads");

            migrationBuilder.DropTable(
                name: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_Threads_CreatedById1",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_ThreadEntryReactions_CreatedById1",
                table: "ThreadEntryReactions");

            migrationBuilder.DropIndex(
                name: "IX_ThreadEntries_CreatedById1",
                table: "ThreadEntries");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedById1",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectOwners",
                table: "ProjectOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectMembers",
                table: "ProjectMembers");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "ThreadEntryReactions");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "ThreadEntries");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "ProjectOwners");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "ProjectMembers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ProjectOwners",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "ProjectMembers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectOwners",
                table: "ProjectOwners",
                columns: new[] { "ApplicationUserId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectMembers",
                table: "ProjectMembers",
                columns: new[] { "ApplicationUserId", "ProjectId" });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Threads_CreatedById",
                table: "Threads",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadEntryReactions_CreatedById",
                table: "ThreadEntryReactions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadEntries_CreatedById",
                table: "ThreadEntries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedById",
                table: "Projects",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectMembers_ApplicationUser_ApplicationUserId",
                table: "ProjectMembers",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOwners_ApplicationUser_ApplicationUserId",
                table: "ProjectOwners",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ApplicationUser_CreatedById",
                table: "Projects",
                column: "CreatedById",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadEntries_ApplicationUser_CreatedById",
                table: "ThreadEntries",
                column: "CreatedById",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadEntryReactions_ApplicationUser_CreatedById",
                table: "ThreadEntryReactions",
                column: "CreatedById",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_ApplicationUser_CreatedById",
                table: "Threads",
                column: "CreatedById",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
