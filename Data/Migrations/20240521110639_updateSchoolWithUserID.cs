using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolFinderWeb.Data.Migrations
{
    public partial class updateSchoolWithUserID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "School",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "School");
        }
    }
}
