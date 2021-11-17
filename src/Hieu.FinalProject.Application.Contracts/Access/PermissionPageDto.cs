using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Access
{
    public class PermissionPageDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { set; get; }
    }
}
