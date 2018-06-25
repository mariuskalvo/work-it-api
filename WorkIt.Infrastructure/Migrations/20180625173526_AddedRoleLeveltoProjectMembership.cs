using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkIt.Infrastructure.Migrations
{
    public partial class AddedRoleLeveltoProjectMembership : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleLevel",
                table: "ProjectMembers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleLevel",
                table: "ProjectMembers");
        }
    }
}
