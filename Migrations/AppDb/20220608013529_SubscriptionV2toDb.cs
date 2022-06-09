using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class SubscriptionV2toDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

          

            

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionId",
                table: "LessonSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "LessonSchedules");

            migrationBuilder.AddColumn<int>(
                name: "HoursRemaining",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonStructureId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_LessonStructureId",
                table: "Subscriptions",
                column: "LessonStructureId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_StudentId",
                table: "Subscriptions",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_LessonStructures_LessonStructureId",
                table: "Subscriptions",
                column: "LessonStructureId",
                principalTable: "LessonStructures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Students_StudentId",
                table: "Subscriptions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
