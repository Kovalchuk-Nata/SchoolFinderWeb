using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolFinderWeb.Data.Migrations
{
    public partial class EditSchools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isFavorite",
                table: "School",
                newName: "isConfirmed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isConfirmed",
                table: "School",
                newName: "isFavorite");
        }
    }
}
