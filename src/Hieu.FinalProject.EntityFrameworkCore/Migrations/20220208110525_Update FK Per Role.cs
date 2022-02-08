using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieu.FinalProject.Migrations
{
    public partial class UpdateFKPerRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppMyPermission_Roles_PermissionID",
                table: "AppMyPermission_Roles",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_AppMyPermission_Roles_RoleID",
                table: "AppMyPermission_Roles",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppMyPermission_Roles_AppMyRoles_RoleID",
                table: "AppMyPermission_Roles",
                column: "RoleID",
                principalTable: "AppMyRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppMyPermission_Roles_AppPermissions_PermissionID",
                table: "AppMyPermission_Roles",
                column: "PermissionID",
                principalTable: "AppPermissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppMyPermission_Roles_AppMyRoles_RoleID",
                table: "AppMyPermission_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppMyPermission_Roles_AppPermissions_PermissionID",
                table: "AppMyPermission_Roles");

            migrationBuilder.DropIndex(
                name: "IX_AppMyPermission_Roles_PermissionID",
                table: "AppMyPermission_Roles");

            migrationBuilder.DropIndex(
                name: "IX_AppMyPermission_Roles_RoleID",
                table: "AppMyPermission_Roles");
        }
    }
}
