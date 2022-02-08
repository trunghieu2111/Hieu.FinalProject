using System;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Permissions
{
    public class Permission: Entity<Guid>
    {
        public string NamePermission { set; get; }
    }
}
