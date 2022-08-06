using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class TeacherAdditionsIIToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccount",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QualificationUrl",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccount",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "QualificationUrl",
                table: "Teachers");
        }
    }
}
