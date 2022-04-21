using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieu.FinalProject.Migrations
{
    public partial class UpdateTbBranch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAcount",
                table: "AppBranchs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "AppBranchs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "AppBranchs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "AppBranchs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalName",
                table: "AppBranchs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AppBranchs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAcount",
                table: "AppBranchs");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "AppBranchs");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "AppBranchs");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "AppBranchs");

            migrationBuilder.DropColumn(
                name: "LegalName",
                table: "AppBranchs");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AppBranchs");
        }
    }
}
