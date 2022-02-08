using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieu.FinalProject.Migrations
{
    public partial class UpdateFKInvoiceTaxBreak : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvoiceTaxBreaks_AppInvoiceHeaders_InvoiceHeaderId",
                table: "AppInvoiceTaxBreaks");

            migrationBuilder.DropIndex(
                name: "IX_AppInvoiceTaxBreaks_InvoiceHeaderId",
                table: "AppInvoiceTaxBreaks");

            migrationBuilder.DropColumn(
                name: "InvoiceHeaderId",
                table: "AppInvoiceTaxBreaks");

            migrationBuilder.CreateIndex(
                name: "IX_AppInvoiceTaxBreaks_InvoiceId",
                table: "AppInvoiceTaxBreaks",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvoiceTaxBreaks_AppInvoiceHeaders_InvoiceId",
                table: "AppInvoiceTaxBreaks",
                column: "InvoiceId",
                principalTable: "AppInvoiceHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInvoiceTaxBreaks_AppInvoiceHeaders_InvoiceId",
                table: "AppInvoiceTaxBreaks");

            migrationBuilder.DropIndex(
                name: "IX_AppInvoiceTaxBreaks_InvoiceId",
                table: "AppInvoiceTaxBreaks");

            migrationBuilder.AddColumn<long>(
                name: "InvoiceHeaderId",
                table: "AppInvoiceTaxBreaks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppInvoiceTaxBreaks_InvoiceHeaderId",
                table: "AppInvoiceTaxBreaks",
                column: "InvoiceHeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInvoiceTaxBreaks_AppInvoiceHeaders_InvoiceHeaderId",
                table: "AppInvoiceTaxBreaks",
                column: "InvoiceHeaderId",
                principalTable: "AppInvoiceHeaders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
