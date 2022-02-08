using System;
using Volo.Abp.Application.Dtos;

namespace Hieu.FinalProject.Access
{
    public class PermissionDto : AuditedEntityDto<Guid>
    {
        public string NamePermission { set; get; }
    }
}
