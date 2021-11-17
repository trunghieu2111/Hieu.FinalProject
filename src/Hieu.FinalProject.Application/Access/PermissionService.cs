using Hieu.FinalProject.Access.Repositories;
using Hieu.FinalProject.Permissions;
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

        /*public async Task<List<PermissionDto>> GetListAsync()
        {
            var queryable = await _permissionDapperRepository.GetListAsync();

            return queryable.Select(s => new PermissionDto
            {
                Id = s.Id,
                Name = s.Name,
                UserPermission = s.UserPermission,
                BranchPermission = s.BranchPermission,
                CustomerPermission = s.CustomerPermission,
                PerPermission = s.PerPermission,
                InvoicePermision = s.InvoicePermision
            })
            .ToList();
        }*/

        public async Task CreateAsync(CreateUpdatePermissionDto input)
        {
            await _permissionDapperRepository.CreateAsync(input);

            /*var permission = new Permission
            {
                BranchPermission = input.PerPermission,

            };

            await _repository.InsertAsync(permission);*/
        }

        public async Task<Permission> GetAsync(Guid id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateAsync(Guid id, CreateUpdatePermissionDto input)
        {
            var permission = await _repository.GetAsync(id);

            permission.Name = input.Name;
            permission.InvoicePermision = input.InvoicePermision;
            permission.PerPermission = input.PerPermission;
            permission.UserPermission = input.UserPermission;
            permission.BranchPermission = input.BranchPermission;
            permission.CustomerPermission = input.CustomerPermission;

            await _repository.UpdateAsync(permission);
        }

        public async Task<PagedResultDto<PermissionDto>> GetListAsync(PermissionPageDto input)
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
        }
        /* .PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();

         Tìm hiểu thêm phần Comment*/
    }
}
