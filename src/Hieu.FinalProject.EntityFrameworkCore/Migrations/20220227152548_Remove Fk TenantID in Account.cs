using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieu.FinalProject.Migrations
{
    public partial class RemoveFkTenantIDinAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAccounts_AppBranchs_TenantId",
                table: "AppAccounts");

            migrationBuilder.DropIndex(
                name: "IX_AppAccounts_TenantId",
                table: "AppAccounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppAccounts_TenantId",
                table: "AppAccounts",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppAccounts_AppBranchs_TenantId",
                table: "AppAccounts",
                column: "TenantId",
                principalTable: "AppBranchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
