using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieu.FinalProject.Migrations
{
    public partial class UpdateAccountPerrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchPermission",
                table: "AppPermissions");

            migrationBuilder.DropColumn(
                name: "CustomerPermission",
                table: "AppPermissions");

            migrationBuilder.DropColumn(
                name: "InvoicePermision",
                table: "AppPermissions");

            migrationBuilder.DropColumn(
                name: "PerPermission",
                table: "AppPermissions");

            migrationBuilder.DropColumn(
                name: "UserPermission",
                table: "AppPermissions");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AppPermissions",
                newName: "NamePermission");

            migrationBuilder.CreateTable(
                name: "AppAccount_Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<long>(type: "bigint", nullable: false),
                    RoleID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppAccount_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMyPermission_Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMyPermission_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppMyRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMyRoles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppAccount_Roles");

            migrationBuilder.DropTable(
                name: "AppMyPermission_Roles");

            migrationBuilder.DropTable(
                name: "AppMyRoles");

            migrationBuilder.RenameColumn(
                name: "NamePermission",
                table: "AppPermissions",
                newName: "Name");

            migrationBuilder.AddColumn<bool>(
                name: "BranchPermission",
                table: "AppPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CustomerPermission",
                table: "AppPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "InvoicePermision",
                table: "AppPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PerPermission",
                table: "AppPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UserPermission",
                table: "AppPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
