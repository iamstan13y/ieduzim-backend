using Microsoft.EntityFrameworkCore.Migrations;

namespace IEduZimAPI.Migrations
{
    public partial class RolePaymentDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentDescription",
                table: "RolePaymentSettings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentDescription",
                table: "RolePaymentSettings");
        }
    }
}
