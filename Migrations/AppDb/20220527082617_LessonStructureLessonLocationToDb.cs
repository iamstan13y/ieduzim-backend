using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class LessonStructureLessonLocationToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LessonLocationId",
                table: "LessonStructures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonStructures_LessonLocationId",
                table: "LessonStructures",
                column: "LessonLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonStructures_LessonLocations_LessonLocationId",
                table: "LessonStructures",
                column: "LessonLocationId",
                principalTable: "LessonLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonStructures_LessonLocations_LessonLocationId",
                table: "LessonStructures");

            migrationBuilder.DropIndex(
                name: "IX_LessonStructures_LessonLocationId",
                table: "LessonStructures");

            migrationBuilder.AlterColumn<string>(
                name: "LessonLocationId",
                table: "LessonStructures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
