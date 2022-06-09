using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class LessonScheduleLessonStructureToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LessonSchedules_LessonStructureId",
                table: "LessonSchedules",
                column: "LessonStructureId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonSchedules_LessonStructures_LessonStructureId",
                table: "LessonSchedules",
                column: "LessonStructureId",
                principalTable: "LessonStructures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonSchedules_LessonStructures_LessonStructureId",
                table: "LessonSchedules");

            migrationBuilder.DropIndex(
                name: "IX_LessonSchedules_LessonStructureId",
                table: "LessonSchedules");
        }
    }
}
