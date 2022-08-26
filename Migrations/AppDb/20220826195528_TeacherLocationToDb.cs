using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class TeacherLocationToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Teachers");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Teachers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
