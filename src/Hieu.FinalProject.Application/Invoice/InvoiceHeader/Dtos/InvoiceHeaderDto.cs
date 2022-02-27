using System;
using System.Collections.Generic;

namespace Hieu.FinalProject.Invoice.InvoiceHeader.Dtos
{
    public class InvoiceHeaderDto
    {
        public long Id { set; get; }
        public Guid TenantId { set; get; }
        public string TaxCodeBuyer { set; get; }
        public string CompanyNameBuyer { set; get; }
        public string AddressBuyer { set; get; }
        public string TaxCodeSeller { set; get; }
        public long CustomerIdSeller { set; get; }
        public string CompanyNameSeller { set; get; }
        public string FulNameSeller { set; get; }
        public string EmailSeller { set; get; }
        public string AddressSeller { set; get; }
        public string BankNumberSeller { set; get; }
        public string NameBankSeller { set; get; }
        public int InvoiceNumber { set; get; }
        public string InvoiceForm { set; get; }
        public string InvoiceSign { set; get; }
        public DateTime InvoiceDay { set; get; }
        public string Payments { set; get; }
        public string InvoiceNote { set; get; }
        public double TotalDiscountBeforeTax { set; get; }
        public double TotalDiscountAfterTax { set; get; }
        public float PercentDiscountAfterTax { set; get; }
        public double TotalProduct { set; get; }
        public double TotalTax { set; get; }
        public double TotalPay { set; get; }

        public List<InvoiceDetailDto> InvoiceDetails { get; set; }
        public List<InvoiceTaxBreakDto> InvoiceTaxBreaks { get; set; }
    }

    public class InvoiceDetailDto
    {
        public long Id { set; get; }
        public long InvoiceId { set; get; }
        public string NameProduct { set; get; }
        public long ProductId { set; get; }
        public string Content { set; get; }
        public string Unit { set; get; }
        public int Quantity { set; get; }
        public double Price { set; get; }
        public int PercentTaxSell { set; get; }
        public float PercentDiscountBeforeTax { set; get; }
        public float PercentMoney { set; get; }
        public double IntoMoney { set; get; }
    }

    public class InvoiceTaxBreakDto
    {
        public long InvoiceId { set; get; }
        public long Id { set; get; }
        public string NameTaxSell { set; get; }
        public int PercentTaxSell { set; get; }
        public double MoneyTaxSell { set; get; }
    }
}
