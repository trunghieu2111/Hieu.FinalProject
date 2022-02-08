using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieu.FinalProject.Migrations
{
    public partial class UpdateFKInvoiceDatails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppInvoiceDetails_InvoiceId",
                table: "AppInvoiceDetails",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvoiceDetails_AppInvoiceHeaders_InvoiceId",
                table: "AppInvoiceDetails",
                column: "InvoiceId",
                principalTable: "AppInvoiceHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvoiceDetails_AppInvoiceHeaders_InvoiceId",
                table: "AppInvoiceDetails");

            migrationBuilder.DropIndex(
                name: "IX_AppInvoiceDetails_InvoiceId",
                table: "AppInvoiceDetails");
        }
    }
}
