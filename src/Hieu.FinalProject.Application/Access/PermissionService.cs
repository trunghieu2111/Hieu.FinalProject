using Hieu.FinalProject.Access.Repositories;
using Hieu.FinalProject.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Hieu.FinalProject.Access
{
    public class PermissionService : ApplicationService
    {
        private readonly IRepository<Permission, Guid> _repository;
        private readonly IPermissionDapperRepository _permissionDapperRepository;
        
        public PermissionService(IRepository<Permission, Guid> repository,
                                IPermissionDapperRepository permissionDapperRepository)
        {
            _repository = repository;
            _permissionDapperRepository = permissionDapperRepository;
        }

        public async Task<List<PermissionDto>> GetListAsync(PermissionPageDto input)
        {
            return await _permissionDapperRepository.GetListAsync(input);
        }

        /*[HttpPost("TestPost")]*/
        public async Task<PermissionDto> CreateAsync(CreateUpdatePermissionDto input)
        {
            return await _permissionDapperRepository.CreateAsync(input);
        }

        public async Task<PermissionDto> GetAsync(Guid id)
        {
            var Permission = await _permissionDapperRepository.GetAsync(id);
            return Permission;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _permissionDapperRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync(Guid id, CreateUpdatePermissionDto input)
        {
            await _permissionDapperRepository.UpdateAsync(id, input);
        }

        /*public async Task<PagedResultDto<PermissionDto>> GetListAsync(PermissionPageDto input)
        {
            var keyword = input.Keyword;
            var query = _repository.AsNoTracking()
                .WhereIf(
                             !string.IsNullOrEmpty(input.Keyword),
                             x => x.Name.Contains(keyword))
                ;
            var currencies = await query.Select(x => ObjectMapper.Map<Permission, PermissionDto>(x)).ToListAsync();

            return new PagedResultDto<PermissionDto>
            {
                TotalCount = await query.CountAsync(),
                Items = currencies
            };
        }*/
        /* .PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();

         Tìm hiểu thêm phần Comment*/
    }
}
