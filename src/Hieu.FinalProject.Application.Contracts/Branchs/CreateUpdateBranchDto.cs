using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Branchs
{
    public class CreateUpdateBranchDto
    {
        public Guid Id { get; set; }

        [Required]
        public string MST { set; get; }

        [Required]
        public string URL { set; get; }

        [Required]
        public string NameBranch { set; get; }

        [Required]
        public string Address { set; get; }

        public Guid ParentId { set; get; }
        public AccountBranchDto AccountBranch { set; get; }
    }

    public class AccountBranchDto : AuditedEntityDto<long>
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Acc { set; get; }
        public string Pass { set; get; }
        public Guid TenantId { set; get; }
    }
}
