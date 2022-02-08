using System;
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
        public Guid TenantId { set; get; }
    }
}
