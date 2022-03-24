using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations
{
    public partial class TeacherActiveDayss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeacherActiveDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DaysAvailable = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherActiveDays", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherActiveDays_UserId",
                table: "TeacherActiveDays",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherActiveDays");
        }
    }
}
