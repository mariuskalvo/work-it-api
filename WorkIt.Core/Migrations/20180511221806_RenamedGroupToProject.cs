using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Core.Migrations
{
    public partial class RenamedGroupToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadEntries_Threads_GroupThreadId",
                table: "ThreadEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Groups_GroupId",
                table: "Threads");

            migrationBuilder.DropTable(
                name: "ApplicationUserOwnedGroups");

            migrationBuilder.DropIndex(
                name: "IX_ThreadEntries_GroupThreadId",
                table: "ThreadEntries");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "Threads",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_GroupId",
                table: "Threads",
                newName: "IX_Threads_ProjectId");

            migrationBuilder.AddColumn<long>(
                name: "ThreadId",
                table: "ThreadEntries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUserOwnedProjects",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    GroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserOwnedProjects", x => new { x.ApplicationUserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserOwnedProjects_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserOwnedProjects_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadEntries_ThreadId",
                table: "ThreadEntries",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserOwnedProjects_GroupId",
                table: "ApplicationUserOwnedProjects",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadEntries_Threads_ThreadId",
                table: "ThreadEntries",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Groups_ProjectId",
                table: "Threads",
                column: "ProjectId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadEntries_Threads_ThreadId",
                table: "ThreadEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Groups_ProjectId",
                table: "Threads");

            migrationBuilder.DropTable(
                name: "ApplicationUserOwnedProjects");

            migrationBuilder.DropIndex(
                name: "IX_ThreadEntries_ThreadId",
                table: "ThreadEntries");

            migrationBuilder.DropColumn(
                name: "ThreadId",
                table: "ThreadEntries");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Threads",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Threads_ProjectId",
                table: "Threads",
                newName: "IX_Threads_GroupId");

            migrationBuilder.CreateTable(
                name: "ApplicationUserOwnedGroups",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    GroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserOwnedGroups", x => new { x.ApplicationUserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserOwnedGroups_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserOwnedGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadEntries_GroupThreadId",
                table: "ThreadEntries",
                column: "GroupThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserOwnedGroups_GroupId",
                table: "ApplicationUserOwnedGroups",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadEntries_Threads_GroupThreadId",
                table: "ThreadEntries",
                column: "GroupThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Groups_GroupId",
                table: "Threads",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
