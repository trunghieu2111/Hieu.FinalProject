using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Accounts
{
    public class Account : Entity<long>
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Acc { set; get; }
        public string Pass { set; get; }

        [ForeignKey("Branch")]
        public Guid TenantId { set; get; }
        public Branchs.Branch Branch { set; get; }
    }
}
