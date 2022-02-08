using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieu.FinalProject.Migrations
{
    public partial class UpdateFKAccRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppAccount_Roles_AccountID",
                table: "AppAccount_Roles",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_AppAccount_Roles_RoleID",
                table: "AppAccount_Roles",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppAccount_Roles_AppAccounts_AccountID",
                table: "AppAccount_Roles",
                column: "AccountID",
                principalTable: "AppAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppAccount_Roles_AppMyRoles_RoleID",
                table: "AppAccount_Roles",
                column: "RoleID",
                principalTable: "AppMyRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAccount_Roles_AppAccounts_AccountID",
                table: "AppAccount_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppAccount_Roles_AppMyRoles_RoleID",
                table: "AppAccount_Roles");

            migrationBuilder.DropIndex(
                name: "IX_AppAccount_Roles_AccountID",
                table: "AppAccount_Roles");

            migrationBuilder.DropIndex(
                name: "IX_AppAccount_Roles_RoleID",
                table: "AppAccount_Roles");
        }
    }
}
