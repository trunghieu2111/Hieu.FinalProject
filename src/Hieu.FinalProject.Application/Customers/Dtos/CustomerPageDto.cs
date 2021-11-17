using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Customers.Dtos
{
    public class CustomerPageDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { set; get; }
    }
}
