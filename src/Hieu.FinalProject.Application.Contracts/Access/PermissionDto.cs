using System;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Access
{
    public class PermissionDto : AuditedEntityDto<Guid>
    {
        public string Name { set; get; }
        public bool UserPermission { set; get; }
        public bool BranchPermission { set; get; }
        public bool CustomerPermission { set; get; }
        public bool PerPermission { set; get; }
        public bool InvoicePermision { set; get; }
    }
}
