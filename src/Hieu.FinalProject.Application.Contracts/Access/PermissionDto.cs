using System;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Access
{
    public class PermissionDto : AuditedEntityDto<Guid>
    {
        public string Name { set; get; }
        public string UserPermission { set; get; }
        public string BranchPermission { set; get; }
        public string CustomerPermission { set; get; }
        public string PerPermission { set; get; }
        public string InvoicePermision { set; get; }
    }
}
