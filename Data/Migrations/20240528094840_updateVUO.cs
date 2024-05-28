using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolFinderWeb.Data.Migrations
{
    public partial class updateVUO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VUO_School_SchoolID",
                table: "VUO");

            migrationBuilder.RenameColumn(
                name: "SchoolID",
                table: "VUO",
                newName: "SchoolId");

            migrationBuilder.RenameIndex(
                name: "IX_VUO_SchoolID",
                table: "VUO",
                newName: "IX_VUO_SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_VUO_School_SchoolId",
                table: "VUO",
                column: "SchoolId",
                principalTable: "School",
                principalColumn: "SchoolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VUO_School_SchoolId",
                table: "VUO");

            migrationBuilder.RenameColumn(
                name: "SchoolId",
                table: "VUO",
                newName: "SchoolID");

            migrationBuilder.RenameIndex(
                name: "IX_VUO_SchoolId",
                table: "VUO",
                newName: "IX_VUO_SchoolID");

            migrationBuilder.AddForeignKey(
                name: "FK_VUO_School_SchoolID",
                table: "VUO",
                column: "SchoolID",
                principalTable: "School",
                principalColumn: "SchoolID");
        }
    }
}
