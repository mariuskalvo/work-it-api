using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Core.Migrations
{
    public partial class RenamedApplicationUserOwnedGroupsToProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserOwnedProjects_Groups_GroupId",
                table: "ApplicationUserOwnedProjects");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "ApplicationUserOwnedProjects",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserOwnedProjects_GroupId",
                table: "ApplicationUserOwnedProjects",
                newName: "IX_ApplicationUserOwnedProjects_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserOwnedProjects_Groups_ProjectId",
                table: "ApplicationUserOwnedProjects",
                column: "ProjectId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserOwnedProjects_Groups_ProjectId",
                table: "ApplicationUserOwnedProjects");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ApplicationUserOwnedProjects",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserOwnedProjects_ProjectId",
                table: "ApplicationUserOwnedProjects",
                newName: "IX_ApplicationUserOwnedProjects_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserOwnedProjects_Groups_GroupId",
                table: "ApplicationUserOwnedProjects",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
