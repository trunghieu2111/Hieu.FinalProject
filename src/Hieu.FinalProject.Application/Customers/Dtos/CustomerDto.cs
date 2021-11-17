using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Customers.Dtos
{
    public class CustomerDto : AuditedEntityDto<long>
    {
        public string TaxCode { set; get; }
        public string Address { set; get; }
        public string Name { set; get; }
        public string City { set; get; }
        public string District { set; get; }
        public string Daidienphapnhan { set; get; }
        public string STK { set; get; }
        public string TenNH { set; get; }
        public string SDT { set; get; }
        public string Fax { set; get; }
        public string Email { set; get; }
    }
}
