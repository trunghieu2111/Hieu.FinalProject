using System;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Permissions
{
    public class Permission: Entity<Guid>
    {
        public string Name { set; get; }
        public bool UserPermission { set; get; }
        public bool BranchPermission { set; get; }
        public bool CustomerPermission { set; get; }
        public bool PerPermission { set; get; }
        public bool InvoicePermision { set; get; }

    }
}
