using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class StudentLocationToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBalance",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Students");

            migrationBuilder.AddColumn<double>(
                name: "AccountBalance",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
