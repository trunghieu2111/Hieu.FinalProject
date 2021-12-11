using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hieu.FinalProject.Migrations
{
    public partial class AddInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppInvoiceDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    NameProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PercentTaxSell = table.Column<int>(type: "int", nullable: false),
                    PercentDiscountBeforeTax = table.Column<float>(type: "real", nullable: false),
                    PercentMoney = table.Column<float>(type: "real", nullable: false),
                    IntoMoney = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInvoiceDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppInvoiceHeaders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxCodeBuyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyNameBuyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressBuyer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxCodeSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerIdSeller = table.Column<long>(type: "bigint", nullable: false),
                    CompanyNameSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FulNameSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankNumberSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameBankSeller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    InvoiceForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Payments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvoiceNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDiscountBeforeTax = table.Column<double>(type: "float", nullable: false),
                    TotalDiscountAfterTax = table.Column<double>(type: "float", nullable: false),
                    PercentDiscountAfterTax = table.Column<float>(type: "real", nullable: false),
                    TotalProduct = table.Column<double>(type: "float", nullable: false),
                    TotalTax = table.Column<double>(type: "float", nullable: false),
                    TotalPay = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInvoiceHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppInvoiceTaxBreaks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    NameTaxSell = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentTaxSell = table.Column<int>(type: "int", nullable: false),
                    MoneyTaxSell = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInvoiceTaxBreaks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInvoiceDetails");

            migrationBuilder.DropTable(
                name: "AppInvoiceHeaders");

            migrationBuilder.DropTable(
                name: "AppInvoiceTaxBreaks");
        }
    }
}
