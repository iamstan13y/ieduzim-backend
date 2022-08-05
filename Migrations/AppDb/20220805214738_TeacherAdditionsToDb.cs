using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations.AppDb
{
    public partial class TeacherAdditionsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QualificationDescription",
                table: "Teachers",
                newName: "ProfilePictureUrl");

            migrationBuilder.RenameColumn(
                name: "ProfilePicture",
                table: "Teachers",
                newName: "PhysicalAddress");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationalQualification",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Occupation",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "EducationalQualification",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Occupation",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Teachers");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                table: "Teachers",
                newName: "QualificationDescription");

            migrationBuilder.RenameColumn(
                name: "PhysicalAddress",
                table: "Teachers",
                newName: "ProfilePicture");
        }
    }
}
