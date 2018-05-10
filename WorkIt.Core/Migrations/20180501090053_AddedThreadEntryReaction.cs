using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Core.Migrations
{
    public partial class AddedThreadEntryReaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThreadEntryReactions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ReactionTag = table.Column<string>(nullable: true),
                    ThreadEntryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadEntryReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadEntryReactions_ThreadEntries_ThreadEntryId",
                        column: x => x.ThreadEntryId,
                        principalTable: "ThreadEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadEntryReactions_ThreadEntryId",
                table: "ThreadEntryReactions",
                column: "ThreadEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThreadEntryReactions");
        }
    }
}
