using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class LessonScheduleIIItoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "LessonSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LessonSchedules_StudentId",
                table: "LessonSchedules",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonSchedules_Students_StudentId",
                table: "LessonSchedules",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonSchedules_Students_StudentId",
                table: "LessonSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LessonSchedules_StudentId",
                table: "LessonSchedules");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "LessonSchedules");
        }
    }
}
