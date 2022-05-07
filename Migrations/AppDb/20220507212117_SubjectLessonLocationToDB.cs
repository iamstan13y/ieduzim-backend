using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class SubjectLessonLocationToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LessonLocationId",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_LessonLocationId",
                table: "Subjects",
                column: "LessonLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_LessonLocations_LessonLocationId",
                table: "Subjects",
                column: "LessonLocationId",
                principalTable: "LessonLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_LessonLocations_LessonLocationId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_LessonLocationId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "LessonLocationId",
                table: "Subjects");
        }
    }
}
