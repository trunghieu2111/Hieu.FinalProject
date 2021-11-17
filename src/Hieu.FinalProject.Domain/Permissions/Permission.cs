using System;
using Volo.Abp.Domain.Entities;

namespace Hieu.FinalProject.Permissions
{
    public class Permission: Entity<Guid>
    {
        public string Name { set; get; }
        public string UserPermission { set; get; }
        public string BranchPermission { set; get; }
        public string CustomerPermission { set; get; }
        public string PerPermission { set; get; }
        public string InvoicePermision { set; get; }

    }
}
