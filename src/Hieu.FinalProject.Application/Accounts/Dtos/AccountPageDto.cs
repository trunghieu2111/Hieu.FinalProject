using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Accounts.Dtos
{
    public class AccountPageDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { set; get; }
    }
}
