using Hieu.FinalProject.Access.Dtos;
using Hieu.FinalProject.Accout_Role;
using Hieu.FinalProject.Permission_Role;
using Hieu.FinalProject.Permissions;
using Hieu.FinalProject.Role;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Hieu.FinalProject.Access
{
    public class AuthenPermissionService : ApplicationService
    {
        private readonly IRepository<Account_Role, long> _accountRoleRepos;
        private readonly IRepository<MyRole, long> _myRoleRepos;
        private readonly IRepository<MyPermission_Role, long> _myPermissionRoleRepos;
        private readonly IRepository<Permission, Guid> _permissionRepos;

        public AuthenPermissionService(IRepository<Account_Role, long> accountRoleRepos,
                                    IRepository<MyRole, long> myRoleRepos,
                                    IRepository<MyPermission_Role, long> myPermissionRoleRepos,
                                    IRepository<Permission, Guid> permissionRepos)
        {
            _accountRoleRepos = accountRoleRepos;
            _myRoleRepos = myRoleRepos;
            _myPermissionRoleRepos = myPermissionRoleRepos;
            _permissionRepos = permissionRepos;
        }

        [HttpGet("api/app/authen-permission/{id}")]
        public async Task<PagedResultDto<PermissionDto>> GetListPermissionAuthen(long id)
        {
            var accoutRole =  new List<Account_Role>();
            foreach(var item in _accountRoleRepos)
            {
                if(item.AccountID == id)
                {
                    accoutRole.Add(item);
                }
            }

            var idAccountRole = accoutRole.Select(x => x.RoleID);
            var myRole =  _myRoleRepos.Where(x => idAccountRole.Contains(x.Id)).Select(x => x.Id);

            //var idMyRole = myRole.Select(x => x.Id);
            var permissionRole = _myPermissionRoleRepos.Where(x => myRole.Contains(x.RoleID)).Select(x => x.PermissionID);

            var permission = _permissionRepos.Where(x => permissionRole.Contains(x.Id)).ToList();
            var permissionDto = ObjectMapper.Map<List<Permission>, List<PermissionDto>>(permission);
            return new PagedResultDto<PermissionDto>
            {
                TotalCount = permissionDto.Count(),
                Items = permissionDto
            };
        }
    }
}
