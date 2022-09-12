using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class UserStatusToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Teachers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Students");
        }
    }
}
