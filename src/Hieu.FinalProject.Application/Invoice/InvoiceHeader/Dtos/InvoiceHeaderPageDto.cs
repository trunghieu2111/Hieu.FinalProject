using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Invoice.InvoiceHeader.Dtos
{
    public class InvoiceHeaderPageDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { set; get; }
        public Guid TenantID { set; get; }
    }
}
