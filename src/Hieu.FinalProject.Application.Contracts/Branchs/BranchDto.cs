using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Branchs
{
    public class BranchDto : AuditedEntityDto<Guid>
    {
        public string MST { set; get; }
        public string URL { set; get; }
        public string NameBranch { set; get; }
        public string Address { set; get; }
        public string ParentId { set; get; }

    }
}
