using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Role
{
    public class MyRole : Entity<long>
    {
        public Guid TenantId { set; get; }
        public string RoleName { set; get; }
    }
}
