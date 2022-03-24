using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations
{
    public partial class TeacherActiveDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Administered",
                table: "LessonRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "LessonRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LessonRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Administered",
                table: "LessonRequests");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "LessonRequests");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LessonRequests");
        }
    }
}
