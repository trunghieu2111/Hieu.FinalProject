using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Invoice.InvoiceTaxBreak
{
    public class InvoiceTaxBreak: Entity<long>
    {
        public long InvoiceId { set; get; }
        public string NameTaxSell{set;get;}
        public int PercentTaxSell { set; get; }
        public double MoneyTaxSell { set; get; }
    }
}
