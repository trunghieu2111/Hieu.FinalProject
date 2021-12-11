using System.ComponentModel.DataAnnotations.Schema;

using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Invoice.InvoiceDetail
{
    [Table("InvoiceDetail")]
    public class InvoiceDetailEntity : Entity<long>
    {
        public long InvoiceId { get; set; }
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
}
