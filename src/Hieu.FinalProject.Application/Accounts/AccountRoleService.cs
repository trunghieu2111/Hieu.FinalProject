using Hieu.FinalProject.Accounts.Dtos;
using Hieu.FinalProject.Accout_Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Hieu.FinalProject.Accounts
{
    public class AccountRoleService: ApplicationService
    {
        private readonly IRepository<Account, long> _accountRepos;
        private readonly IRepository<Account_Role, long> _accountRoleRepos;

        public AccountRoleService(IRepository<Account, long> accountRepos,
                                    IRepository<Account_Role, long> accountRoleRepos)
        {
            _accountRepos = accountRepos;
            _accountRoleRepos = accountRoleRepos;
        }

        public async Task<AccountNewDto> CreateAsync(AccountNewDto input)
        {
            var flagAcc = 0;
            foreach(var item in _accountRepos)
            {
                if(item.Acc == input.Acc && item.TenantId == input.TenantId)
                {
                    flagAcc = 1;
                    break;
                }
            }

            if(flagAcc == 0)
            {
                var account = new Account
                {
                    Name = input.Name,
                    Email = input.Email,
                    Phone = input.Phone,
                    Acc = input.Acc,
                    Pass = input.Pass,
                    TenantId = input.TenantId,
                    LockStatus = true
                };

                await _accountRepos.InsertAsync(account);

                //lấy id của account
                await UnitOfWorkManager.Current.SaveChangesAsync();

                var idAccount = account.Id;

                var accountRolesEntity = new List<Account_Role>();
                if (!input.AccountRoles.Any())
                {
                    // trả ra lỗi vì không được để trống
                }

                foreach (var item in input.AccountRoles)
                {
                    var accountRole = new Account_Role
                    {
                        RoleID = item.RoleID,
                        AccountID = idAccount
                    };

                    accountRolesEntity.Add(accountRole);
                }

                await _accountRoleRepos.InsertManyAsync(accountRolesEntity);

                return ObjectMapper.Map<Account, AccountNewDto>(account); //ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.
            }
            else
            {
                var account = new Account
                {
                   Acc  = "0"
                };
                return ObjectMapper.Map<Account, AccountNewDto>(account);
            }
            /*var account = new Account
            {
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                Acc = input.Acc,
                Pass = input.Pass,
                TenantId = input.TenantId
            };

            await _accountRepos.InsertAsync(account);

            //lấy id của account
            await UnitOfWorkManager.Current.SaveChangesAsync();

            var idAccount = account.Id;

            var accountRolesEntity = new List<Account_Role>();
            if (!input.AccountRoles.Any())
            {
                // trả ra lỗi vì không được để trống
            }

            foreach (var item in input.AccountRoles)
            {
                var accountRole = new Account_Role
                {
                    RoleID = item.RoleID,
                    AccountID = idAccount
                };

                accountRolesEntity.Add(accountRole);
            }

            await _accountRoleRepos.InsertManyAsync(accountRolesEntity);

            return ObjectMapper.Map<Account, AccountNewDto>(account); //ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.*/
        }

        public async Task<AccountNewDto> UpdateAsync(long id, AccountNewDto input)
        {
            var account = await _accountRepos.GetAsync(id);
            account.Name = input.Name;
            account.Email = input.Email;
            account.Phone = input.Phone;
            account.Pass = input.Pass;
            account.TenantId = input.TenantId;

            await _accountRepos.UpdateAsync(account);
            //update role_permission

            if (!input.AccountRoles.Any())
            {
                /*throw new UserFriendlyException("Không được trống!");*/
            }


            foreach (var inputItem in input.AccountRoles)
            {
                //thêm mới 
                if (inputItem.Id == 0)
                {
                    var accountRole = new Account_Role
                    {
                        RoleID = inputItem.RoleID,
                        AccountID = id
                    };
                    await _accountRoleRepos.InsertAsync(accountRole);
                }
            }

            //xóa AccountRole

            var lengthAccountRoleDto = 0;
            var accountRoleDto = new List<AccountRoleDto>();
            foreach (var inputItem in input.AccountRoles)
            {
                //lấy ra những phần tử Dto sửa thôi.
                if (inputItem.Id != 0)
                {
                    lengthAccountRoleDto += 1;
                    accountRoleDto.Add(inputItem);
                }

            }

            //await UnitOfWorkManager.Current.SaveChangesAsync();

            var accountRoleEntity = from accountRole in _accountRoleRepos
                                         where accountRole.AccountID == id
                                         select accountRole;
            var lengthAccountRoleEntity = 0;

            foreach (var ItemEntity in accountRoleEntity)
            {
                lengthAccountRoleEntity += 1;
            }

            if (lengthAccountRoleDto < lengthAccountRoleEntity)
            {
                var ItemMapper = ObjectMapper.Map<List<AccountRoleDto>, List<Account_Role>>(accountRoleDto).Select(x => x.Id);
                try
                {
                    var except = accountRoleEntity.Where(x => !ItemMapper.Contains(x.Id)).ToList();
                    await _accountRoleRepos.DeleteManyAsync(except);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return ObjectMapper.Map<Account, AccountNewDto>(account);//ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.

        }

        [HttpGet("api/app/account/lock-account")]
        public async Task<AccountNewDto> LockAccount(long id)
        {
            var account = await _accountRepos.GetAsync(id);
            account.LockStatus = false;
            return ObjectMapper.Map<Account, AccountNewDto>(account);
        }

        [HttpGet("api/app/account/unlock-account")]
        public async Task<AccountNewDto> UnLockAccount(long id)
        {
            var account = await _accountRepos.GetAsync(id);
            account.LockStatus = true;
            return ObjectMapper.Map<Account, AccountNewDto>(account);
        }

        public async Task<AccountNewDto> DeleteAsync(long id)
        {
            var account = await _accountRepos.GetAsync(id);
            await _accountRepos.DeleteAsync(id);

            //Del AccountRole
            var queryAccountRole = from accountRole in _accountRoleRepos
                                      where accountRole.RoleID == id
                                      select accountRole;

            await _accountRoleRepos.DeleteManyAsync(queryAccountRole);

            return ObjectMapper.Map<Account, AccountNewDto>(account);
        }

        public async Task<AccountNewDto> GetAsync(long id)
        {
            var query = await _accountRepos.FirstOrDefaultAsync(x => x.Id == id);
            var accountDto = ObjectMapper.Map<Account, AccountNewDto>(query);

            var accountRoleDtos = new List<AccountRoleDto>();
            accountRoleDtos = await _accountRoleRepos.Where(x => x.AccountID == id).Select(x => ObjectMapper.Map<Account_Role, AccountRoleDto>(x)).ToListAsync();

            var accountDtos = new AccountNewDto();
            accountDtos.AccountRoles = accountRoleDtos;

            accountDtos.Id = id;
            accountDtos.Name = accountDto.Name;
            accountDtos.Email = accountDto.Email;
            accountDtos.Phone = accountDto.Phone;
            accountDtos.Acc = accountDto.Acc;
            accountDtos.Pass = accountDto.Pass;
            accountDtos.TenantId = accountDto.TenantId;
            accountDtos.LockStatus = accountDto.LockStatus;

            return accountDtos;
        }

        public async Task<PagedResultDto<AccountNewDto>> GetListAsync(AccountPageDto input)
        {
            var accountParentId = _accountRepos.Where(x => x.TenantId == input.TenantID);
            var keyword = input.Keyword;
            var query = accountParentId.AsNoTracking()
                .WhereIf(
                             !string.IsNullOrEmpty(input.Keyword),
                             x => x.Name.Contains(keyword)
                             || x.Email.Contains(keyword)
                             || x.Acc.Contains(keyword))
                .OrderByDescending(x => x.Id)
                ;
            var account = await query.Select
                (x => ObjectMapper.Map<Account, AccountNewDto>(x)).ToListAsync();
            return new PagedResultDto<AccountNewDto>
            {
                TotalCount = await query.CountAsync(),
                Items = account
            };
        }
    }
}
