using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolFinderWeb.Data.Migrations
{
    public partial class editSchool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Classes",
                table: "School",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Format",
                table: "School",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Maps",
                table: "School",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PaticipationVUO",
                table: "School",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Shelter",
                table: "School",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Classes",
                table: "School");

            migrationBuilder.DropColumn(
                name: "Format",
                table: "School");

            migrationBuilder.DropColumn(
                name: "Maps",
                table: "School");

            migrationBuilder.DropColumn(
                name: "PaticipationVUO",
                table: "School");

            migrationBuilder.DropColumn(
                name: "Shelter",
                table: "School");
        }
    }
}
