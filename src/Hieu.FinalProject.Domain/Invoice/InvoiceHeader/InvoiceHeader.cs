using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Invoice.InvoiceHeader
{
    public class InvoiceHeader : Entity<long>
    {
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
    }
}
