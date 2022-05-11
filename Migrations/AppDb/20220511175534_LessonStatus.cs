using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class LessonStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Lessons");

            migrationBuilder.AddColumn<int>(
                name: "LessonStatus",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonStatus",
                table: "Lessons");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Lessons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
