using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Core.Migrations
{
    public partial class AddedCreatedByToThreadEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ThreadEntryReactions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadEntryReactions_CreatedById",
                table: "ThreadEntryReactions",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ThreadEntryReactions_AspNetUsers_CreatedById",
                table: "ThreadEntryReactions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThreadEntryReactions_AspNetUsers_CreatedById",
                table: "ThreadEntryReactions");

            migrationBuilder.DropIndex(
                name: "IX_ThreadEntryReactions_CreatedById",
                table: "ThreadEntryReactions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ThreadEntryReactions");
        }
    }
}
