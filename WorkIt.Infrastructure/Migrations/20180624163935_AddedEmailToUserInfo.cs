using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkIt.Infrastructure.Migrations
{
    public partial class AddedEmailToUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserInfos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserInfos");
        }
    }
}
