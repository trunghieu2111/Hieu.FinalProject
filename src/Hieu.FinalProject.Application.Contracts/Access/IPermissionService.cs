using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Hieu.FinalProject.Access
{
    public interface IPermissionService : ICrudAppService<PermissionDto, Guid,
        PermissionPageDto, CreateUpdatePermissionDto>
    {
    }
}
