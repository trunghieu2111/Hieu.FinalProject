using Hieu.FinalProject.Accounts;
using Hieu.FinalProject.Accout_Role;
using Hieu.FinalProject.Permission_Role;
using Hieu.FinalProject.Permissions;
using Hieu.FinalProject.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Hieu.FinalProject.Branchs
{
    //đầu tiên
    public class BranchService : ApplicationService
    {
        private readonly IRepository<Account, long> _accountRepos;
        private readonly IRepository<Branch, Guid> _branchRepos;
        private readonly IRepository<MyRole, long> _myRoleRepos;
        private readonly IRepository<Account_Role, long> _accountRoleRepos;
        private readonly IRepository<MyPermission_Role, long> _permissionRoleRepos;
        private readonly IRepository<Permission, Guid> _permissionRepos;

        public BranchService(IRepository<Account, long> accountRepos,
                                    IRepository<Branch, Guid> branchRepos,
                                    IRepository<MyRole, long> myRoleRepos,
                                    IRepository<Account_Role, long> accountRoleRepos,
                                    IRepository<Permission, Guid> permissionRepos,
                                    IRepository<MyPermission_Role, long> permissionRoleRepos)
        {
            _accountRepos = accountRepos;
            _branchRepos = branchRepos;
            _myRoleRepos = myRoleRepos;
            _accountRoleRepos = accountRoleRepos;
            _permissionRoleRepos = permissionRoleRepos;
            _permissionRepos = permissionRepos;
        }

        public async Task<CreateUpdateBranchDto> CreateAsync(CreateUpdateBranchDto input)
        {
            var branch = new Branch
            {
                MST = input.MST,
                URL = input.URL,
                NameBranch = input.NameBranch,
                Address = input.Address,
                ParentId = input.ParentId
            };
            await _branchRepos.InsertAsync(branch);
            //lấy id của branch
            await UnitOfWorkManager.Current.SaveChangesAsync();
            var idBranch = branch.Id;

            //lưu tài khoản admin của chi nhánh
            var account = new Account
            {
                Name = input.AccountBranch.Name,
                Email = input.AccountBranch.Email,
                Acc = input.AccountBranch.Acc,
                Pass = input.AccountBranch.Pass,
                TenantId = idBranch
            };
            await _accountRepos.InsertAsync(account);
            //lấy id của account
            await UnitOfWorkManager.Current.SaveChangesAsync();
            var idAccount = account.Id;

            //lưu Quyền admin vào bảng Role
            var myRole = new MyRole
            {
                RoleName = "Admin",
                TenantId = idBranch
            };
            await _myRoleRepos.InsertAsync(myRole);
            //lấy id của account
            await UnitOfWorkManager.Current.SaveChangesAsync();
            var idRole = myRole.Id;

            //Lưu vào Account_Role
            var accountRole = new Account_Role
            {
                AccountID = idAccount,
                RoleID = idRole
            };
            await _accountRoleRepos.InsertAsync(accountRole);

            //Lưu vào Permission_Role
            var idPermissions = _permissionRepos.Select(x => x.Id);
            foreach(var i in idPermissions)
            {
                var permissionRole = new MyPermission_Role
                {
                    RoleID = idRole,
                    PermissionID = i
                };
                await _permissionRoleRepos.InsertAsync(permissionRole);
            }

            return ObjectMapper.Map<Branch, CreateUpdateBranchDto>(branch); //ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.
        }

        public async Task DeleteAsync(Guid id)
        {
            await _branchRepos.DeleteAsync(id);
            //del tài khoản thuộc chi nhánh đã xóa
            var account = _accountRepos.AsNoTracking().Where(x => x.TenantId == id);
            var idAccount = account.Select(x => x.Id);
            var accountRole= _accountRoleRepos.Where(x => idAccount.Contains(x.AccountID)).ToList();
            //await _accountRoleRepos.DeleteManyAsync(except);

            await _accountRepos.DeleteManyAsync(account);
            //del Role
            var myRole = _myRoleRepos.AsNoTracking().Where(x => x.TenantId == id);
            //lấy PermissionRole
            var idMyRole = myRole.Select(x => x.Id);
            var permissionRole = _permissionRoleRepos.Where(x => idMyRole.Contains(x.RoleID)).ToList();

            await _myRoleRepos.DeleteManyAsync(myRole);
            //del Account_Role
            await _accountRoleRepos.DeleteManyAsync(accountRole);
            await _permissionRoleRepos.DeleteManyAsync(permissionRole);
        }

        public async Task<PagedResultDto<BranchDto>> GetListAsync(BranchPageDto input)
        {
            var branchParentId = _branchRepos.Where(x => x.ParentId == input.TenantID);
            var keyword = input.Keyword;
            var query = branchParentId.AsNoTracking()
                .WhereIf(
                             !string.IsNullOrEmpty(input.Keyword),
                             x => x.NameBranch.Contains(keyword)
                             || x.Address.Contains(keyword)
                             || x.MST.Contains(keyword))
                ;
            var branches = await query.Select
                (x => ObjectMapper.Map<Branch, BranchDto>(x)).ToListAsync();
            return new PagedResultDto<BranchDto>
            {
                TotalCount = await query.CountAsync(),
                Items = branches
            };
        }

        public async Task<BranchDto> GetAsync(Guid id)
        {
            var query = await _branchRepos.FirstOrDefaultAsync(x => x.Id == id);
            return ObjectMapper.Map<Branch, BranchDto>(query);
        }

        [HttpGet("api/app/login/branch/{taxcode}")]
        public async Task<BranchDto> GetBranch(string taxcode)
        {
            var branch = new BranchDto
            {
                MST = "0"
            };

            foreach (var tenant in _branchRepos)
            {
                if (taxcode == tenant.MST)
                {
                    branch = ObjectMapper.Map<Branch, BranchDto>(tenant);
                    break;
                }
            }
            return branch;
        }

        public async Task<BranchDto> UpdateAsync(Guid id, BranchDto input)
        {
            var branch = await _branchRepos.GetAsync(id);
            branch.URL = input.URL;
            branch.MST = input.MST;
            branch.NameBranch = input.NameBranch;
            branch.Address = input.Address;

            await _branchRepos.UpdateAsync(branch);
            return ObjectMapper.Map<Branch, BranchDto>(branch);
        }
    }

}
