using Hieu.FinalProject.Accounts.Dtos;
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
    public class AccountService : CrudAppService<
        Account,
        AccountDto,
        long,
        AccountPageDto,
        CreateUpdateAccountDto>,
        IAccountService
    {
        private readonly IRepository<Account, long> _repository;
        public AccountService(IRepository<Account, long> repository) : base(repository)
        {
            _repository = repository;

        }

        public override Task<AccountDto> CreateAsync(CreateUpdateAccountDto input)
        {
            return base.CreateAsync(input);
        }

        public override Task DeleteAsync(long id)
        {
            return base.DeleteAsync(id);
        }

        public override Task<AccountDto> GetAsync(long id)
        {
            return base.GetAsync(id);
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
                (x => ObjectMapper.Map<Account, AccountDto>(x)).PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();
            return new PagedResultDto<AccountDto>
            {
                TotalCount = await query.CountAsync(),
                Items = currencies
            };
        }

        public override Task<AccountDto> UpdateAsync(long id, CreateUpdateAccountDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }
}
