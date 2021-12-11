using Hieu.FinalProject.Accounts.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Hieu.FinalProject.Accounts
{
    public class MyAccountService : CrudAppService<
        Account,
        AccountDto,
        long,
        AccountPageDto,
        CreateUpdateAccountDto>,
        IAccountService
    {
        private readonly IRepository<Account, long> _repository;
        public MyAccountService(IRepository<Account, long> repository) : base(repository)
        {
            _repository = repository;

        }

        public override async Task<AccountDto> CreateAsync(CreateUpdateAccountDto input)
        {
            var account = new Account
            {
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                Acc = input.Acc,
                Pass = input.Pass,
                PermissionId = input.PermissionId

            };

            await _repository.InsertAsync(account);
            return ObjectMapper.Map<Account, AccountDto>(account); //ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.
        }

        public override async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        //[HttpGet("{id}")]
        public override async Task<AccountDto> GetAsync(long id)
        {
            var query = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            return ObjectMapper.Map<Account, AccountDto>(query);
            /*return await _repository.GetAsync(id);*/
        }

        public override async Task<PagedResultDto<AccountDto>> GetListAsync(AccountPageDto input)
        {
            var keyword = input.Keyword;
            var query = _repository.AsNoTracking()
                .WhereIf(
                             !string.IsNullOrEmpty(input.Keyword),
                             x => x.Name.Contains(keyword)
                             || x.Email.Contains(keyword)
                             || x.Acc.Contains(keyword))
                ;
            var currencies = await query.Select
                (x => ObjectMapper.Map<Account, AccountDto>(x)).ToListAsync();
            return new PagedResultDto<AccountDto>
            {
                TotalCount = await query.CountAsync(),
                Items = currencies
            };
        }

        public override async Task<AccountDto> UpdateAsync(long id, CreateUpdateAccountDto input)
        {
            var account = await _repository.GetAsync(id);

            account.Name = input.Name;
            account.Email = input.Email;
            account.Phone = input.Phone;
            account.Acc = input.Acc;
            account.Pass = input.Pass;
            account.PermissionId = input.PermissionId;

            await _repository.UpdateAsync(account);
            return ObjectMapper.Map<Account, AccountDto>(account);
        }
    }
}
