using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Core.Migrations
{
    public partial class AddedDescriptionAndProjectOwnerToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserOwnedProjects_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserOwnedProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserOwnedProjects_Projects_ProjectId",
                table: "ApplicationUserOwnedProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserOwnedProjects",
                table: "ApplicationUserOwnedProjects");

            migrationBuilder.RenameTable(
                name: "ApplicationUserOwnedProjects",
                newName: "ProjectOwners");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserOwnedProjects_ProjectId",
                table: "ProjectOwners",
                newName: "IX_ProjectOwners_ProjectId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectOwners",
                table: "ProjectOwners",
                columns: new[] { "ApplicationUserId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOwners_AspNetUsers_ApplicationUserId",
                table: "ProjectOwners",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectOwners_Projects_ProjectId",
                table: "ProjectOwners",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOwners_AspNetUsers_ApplicationUserId",
                table: "ProjectOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectOwners_Projects_ProjectId",
                table: "ProjectOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectOwners",
                table: "ProjectOwners");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.RenameTable(
                name: "ProjectOwners",
                newName: "ApplicationUserOwnedProjects");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectOwners_ProjectId",
                table: "ApplicationUserOwnedProjects",
                newName: "IX_ApplicationUserOwnedProjects_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserOwnedProjects",
                table: "ApplicationUserOwnedProjects",
                columns: new[] { "ApplicationUserId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserOwnedProjects_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserOwnedProjects",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserOwnedProjects_Projects_ProjectId",
                table: "ApplicationUserOwnedProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
