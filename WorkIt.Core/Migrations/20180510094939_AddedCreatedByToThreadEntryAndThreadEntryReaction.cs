using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Core.Migrations
{
    public partial class AddedCreatedByToThreadEntryAndThreadEntryReaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ThreadEntries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadEntries_CreatedById",
                table: "ThreadEntries",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadEntries_AspNetUsers_CreatedById",
                table: "ThreadEntries",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadEntries_AspNetUsers_CreatedById",
                table: "ThreadEntries");

            migrationBuilder.DropIndex(
                name: "IX_ThreadEntries_CreatedById",
                table: "ThreadEntries");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ThreadEntries");
        }
    }
}
