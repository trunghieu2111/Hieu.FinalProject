using Hieu.FinalProject.Access;
using Hieu.FinalProject.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Hieu.FinalProject.Access.Repositories
{
    public interface IPermissionDapperRepository
    {
        Task<List<Permission>> GetListAsync();
        Task CreateAsync(CreateUpdatePermissionDto permissionDto);
    }
}
