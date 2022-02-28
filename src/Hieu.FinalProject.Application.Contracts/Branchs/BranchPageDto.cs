﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Branchs
{
    public class BranchPageDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { set; get; }
        public Guid TenantID { set; get; }
    }
}
