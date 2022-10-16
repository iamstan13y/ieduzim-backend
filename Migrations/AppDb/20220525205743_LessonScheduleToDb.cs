using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class LessonScheduleToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "LessonSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonStructureId = table.Column<int>(type: "int", nullable: false),
                    LessonDay = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonSchedules", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonStructures_Level_LevelId",
                table: "LessonStructures");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonStructures_Subjects_SubjectId",
                table: "LessonStructures");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonStructures_Teachers_TeacherId",
                table: "LessonStructures");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_LessonStructures_LessonStructureId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "LessonSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonStructures",
                table: "LessonStructures");

            migrationBuilder.RenameTable(
                name: "LessonStructures",
                newName: "LessonStructure");

            migrationBuilder.RenameIndex(
                name: "IX_LessonStructures_TeacherId",
                table: "LessonStructure",
                newName: "IX_LessonStructure_TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonStructures_SubjectId",
                table: "LessonStructure",
                newName: "IX_LessonStructure_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonStructures_LevelId",
                table: "LessonStructure",
                newName: "IX_LessonStructure_LevelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonStructure",
                table: "LessonStructure",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonStructure_Level_LevelId",
                table: "LessonStructure",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonStructure_Subjects_SubjectId",
                table: "LessonStructure",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonStructure_Teachers_TeacherId",
                table: "LessonStructure",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_LessonStructure_LessonStructureId",
                table: "Subscriptions",
                column: "LessonStructureId",
                principalTable: "LessonStructure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
