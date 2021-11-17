using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Hieu.FinalProject.Branchs
{
    //đầu tiên
    public class BranchService : CrudAppService<
        Branch,
        BranchDto,   
        Guid,
        BranchPageDto,
        CreateUpdateBranchDto>, 
        IBranchService
    {

        private readonly IRepository<Branch, Guid> _repository;

        public BranchService(IRepository<Branch, Guid> repository) : base(repository)
        {
            _repository = repository;

        }

        public override Task<BranchDto> CreateAsync(CreateUpdateBranchDto input)
        {
            return base.CreateAsync(input);
        }

        public override Task DeleteAsync(Guid id)
        {
            return base.DeleteAsync(id);
        }

        public override async Task<PagedResultDto<BranchDto>> GetListAsync(BranchPageDto input)
        {
            var keyword = input.Keyword;
            var query = _repository.AsNoTracking()
                .WhereIf(
                             !string.IsNullOrEmpty(input.Keyword),
                             x => x.NameBranch.Contains(keyword)
                             || x.Address.Contains(keyword)
                             || x.MST.Contains(keyword)
                             || x.URL.Contains(keyword)) 
                ;
            var currencies = await query.Select
                (x => ObjectMapper.Map<Branch, BranchDto>(x)).PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();
            return new PagedResultDto<BranchDto>
            {
                TotalCount = await query.CountAsync(),
                Items = currencies
            };
        }

        public override Task<BranchDto> GetAsync(Guid id)
        {
            return base.GetAsync(id);
        }

        public override Task<BranchDto> UpdateAsync(Guid id, CreateUpdateBranchDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }

}
