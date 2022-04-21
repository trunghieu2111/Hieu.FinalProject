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
        public Guid ParentId { set; get; }
        public string LegalName { set; get; }
        public string BankAcount { set; get; }
        public string BankName { set; get; }
        public string Phone { set; get; }
        public string Fax { set; get; }
        public string Email { set; get; }

    }
}
