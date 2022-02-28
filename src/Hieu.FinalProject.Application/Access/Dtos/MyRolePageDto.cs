using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Access.Dtos
{
    public class MyRolePageDto: PagedAndSortedResultRequestDto
    {
        public string Keyword { set; get; }
        public Guid TenantID { set; get; }
    }
}
