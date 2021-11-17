using System;
using Volo.Abp.Application.Services;

namespace Hieu.FinalProject.Accounts.Dtos
{
    public interface IAccountService : ICrudAppService<AccountDto, long,
        AccountPageDto, CreateUpdateAccountDto>
    {
    }
}
