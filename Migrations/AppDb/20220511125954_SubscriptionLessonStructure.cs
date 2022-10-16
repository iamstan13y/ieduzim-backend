using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class SubscriptionLessonStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Subjects_SubjectId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Subscriptions",
                newName: "LessonStructureId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_SubjectId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_LessonStructureId");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Lessons",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Lessons",
                newName: "LessonDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Lessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "LessonStructure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    LessonLocationId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamTypeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonStructure", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonStructure_Level_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonStructure_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonStructure_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonStructure_LevelId",
                table: "LessonStructure",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonStructure_SubjectId",
                table: "LessonStructure",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonStructure_TeacherId",
                table: "LessonStructure",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_LessonStructure_LessonStructureId",
                table: "Subscriptions",
                column: "LessonStructureId",
                principalTable: "LessonStructure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_LessonStructure_LessonStructureId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "LessonStructure");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "LessonStructureId",
                table: "Subscriptions",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_LessonStructureId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_SubjectId");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Lessons",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "LessonDate",
                table: "Lessons",
                newName: "EndDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Subjects_SubjectId",
                table: "Subscriptions",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
