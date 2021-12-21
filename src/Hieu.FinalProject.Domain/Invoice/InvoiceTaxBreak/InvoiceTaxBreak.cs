using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Invoice.InvoiceTaxBreak
{
    [Table("InvoiceTaxBreak")]
    public class InvoiceTaxBreakEntity: Entity<long>
    {
        public long InvoiceId { set; get; }
        public string NameTaxSell{set;get;}
        public int PercentTaxSell { set; get; }
        public double MoneyTaxSell { set; get; }
    }
}
