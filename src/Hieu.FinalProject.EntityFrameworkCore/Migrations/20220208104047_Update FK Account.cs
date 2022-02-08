using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieu.FinalProject.Migrations
{
    public partial class UpdateFKAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ParentId",
                table: "AppBranchs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAccounts_AppBranchs_TenantId",
                table: "AppAccounts");

            migrationBuilder.DropIndex(
                name: "IX_AppAccounts_TenantId",
                table: "AppAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "AppBranchs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
