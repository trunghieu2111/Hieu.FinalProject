using Hieu.FinalProject.Access.Dtos;
using Hieu.FinalProject.Access.Repositories;
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

namespace Hieu.FinalProject.Access
{
    public class PermissionService : ApplicationService
    {
        private readonly IRepository<MyPermission_Role, long> _myPermissionRoleRepos;
        private readonly IRepository<MyRole, long> _myRoleRepos;

        public PermissionService(IRepository<MyPermission_Role, long> myPermissionRoleRepos,
                                    IRepository<MyRole, long> myRoleRepos)
        {
            _myPermissionRoleRepos = myPermissionRoleRepos;
            _myRoleRepos = myRoleRepos;
        }

        public async Task<MyRoleDto> CreateAsync(MyRoleDto input)
        {
            var myRole = new MyRole
            {
                TenantId = input.TenantId,
                RoleName = input.RoleName
            };

            await _myRoleRepos.InsertAsync(myRole);

            //lấy id của Role
            await UnitOfWorkManager.Current.SaveChangesAsync();

            var idRole = myRole.Id;

            var myPermissionRoles = new List<MyPermission_Role>();
            if (!input.MyPermissionRoles.Any())
            {
                // trả ra lỗi vì không được để quyền trống
            }

            foreach (var item in input.MyPermissionRoles)
            {
                var myPermissionRole = new MyPermission_Role
                {
                    RoleID = idRole,
                    PermissionID = item.PermissionID
                };

                myPermissionRoles.Add(myPermissionRole);
            }

            await _myPermissionRoleRepos.InsertManyAsync(myPermissionRoles);

            return ObjectMapper.Map<MyRole, MyRoleDto>(myRole); //ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.
        }

        public async Task<MyRoleDto> UpdateAsync(long id, MyRoleDto input)
        {
            var myRole = await _myRoleRepos.GetAsync(id);

            myRole.RoleName = input.RoleName;
            myRole.TenantId = input.TenantId;

            await _myRoleRepos.UpdateAsync(myRole);
            //update role_permission

            if (!input.MyPermissionRoles.Any())
            {
                /*throw new UserFriendlyException("Không được trống!");*/
            }


            foreach (var inputItem in input.MyPermissionRoles)
            {
                //thêm mới 
                if (inputItem.Id == 0)
                {
                    var myPermissionRole = new MyPermission_Role
                    {
                        RoleID = id,
                        PermissionID = inputItem.PermissionID
                    };
                    await _myPermissionRoleRepos.InsertAsync(myPermissionRole);
                }
                /*
                //sửa
                else if (inputItem.Id != 0)
                {
                    var myPermissionRole = await _myPermissionRoleRepos.FirstOrDefaultAsync(x => x.Id == inputItem.Id);
                    if (myPermissionRole == null)
                    {
                        continue;
                        //nhảy ra rồi lặp lại.
                    }
                    invoiceDetailEntity.InvoiceId = id;
                    invoiceDetailEntity.NameProduct = inputItem.NameProduct;
                    invoiceDetailEntity.ProductId = inputItem.ProductId;
                    invoiceDetailEntity.Content = inputItem.Content;
                    invoiceDetailEntity.Unit = inputItem.Unit;
                    invoiceDetailEntity.Quantity = inputItem.Quantity;
                    invoiceDetailEntity.Price = inputItem.Price;
                    invoiceDetailEntity.PercentTaxSell = inputItem.PercentTaxSell;
                    invoiceDetailEntity.PercentDiscountBeforeTax = inputItem.PercentDiscountBeforeTax;
                    invoiceDetailEntity.PercentMoney = inputItem.PercentMoney;
                    invoiceDetailEntity.IntoMoney = inputItem.IntoMoney;

                    await _invoiceDetailRepos.UpdateAsync(invoiceDetailEntity);
                }*/
            }
 
            //xóa InvoiceDetail

            var lengthPermissionRoleDto = 0;
            var myPermissionRoleDto = new List<MyPermissionRoleDto>();
            foreach (var inputItem in input.MyPermissionRoles)
            {
                //lấy ra những phần tử Dto sửa thôi.
                if (inputItem.Id != 0)
                {
                        lengthPermissionRoleDto += 1;
                        myPermissionRoleDto.Add(inputItem);
                }

            }

            //await UnitOfWorkManager.Current.SaveChangesAsync();

            var myPermissionRoleEntity = from myPermissionRole in _myPermissionRoleRepos
                                       where myPermissionRole.RoleID == id
                                       select myPermissionRole;
            var lengthPermissionRoleEntity = 0;

            foreach (var ItemEntity in myPermissionRoleEntity)
            {
                    lengthPermissionRoleEntity += 1;
            }

            if (lengthPermissionRoleDto < lengthPermissionRoleEntity)
            {
                var ItemMapper = ObjectMapper.Map<List<MyPermissionRoleDto>, List<MyPermission_Role>>(myPermissionRoleDto).Select(x => x.Id);
                try
                {
                    var except = myPermissionRoleEntity.Where(x => !ItemMapper.Contains(x.Id)).ToList();
                    await _myPermissionRoleRepos.DeleteManyAsync(except);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return ObjectMapper.Map<MyRole, MyRoleDto>(myRole); //ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.

        }
        public async Task<MyRoleDto> DeleteAsync(long id)
        {
            var myRole = await _myRoleRepos.GetAsync(id);
            await _myRoleRepos.DeleteAsync(id);

            //Del PermissionRole
            var queryPermissionRole = from myPermissionRole in _myPermissionRoleRepos
                              where myPermissionRole.RoleID == id
                              select myPermissionRole;

            await _myPermissionRoleRepos.DeleteManyAsync(queryPermissionRole);

            return ObjectMapper.Map<MyRole, MyRoleDto>(myRole);
        }

        public async Task<MyRoleDto> GetAsync(long id)
        {
            var query = await _myRoleRepos.FirstOrDefaultAsync(x => x.Id == id);
            var myRoleDto = ObjectMapper.Map<MyRole, MyRoleDto>(query);

            var myPermissionRoleDtos = new List<MyPermissionRoleDto>();
            myPermissionRoleDtos = await _myPermissionRoleRepos.Where(x => x.RoleID == id).Select(x => ObjectMapper.Map<MyPermission_Role, MyPermissionRoleDto>(x)).ToListAsync();
        
            var myRoleDtos = new MyRoleDto();
            myRoleDtos.MyPermissionRoles = myPermissionRoleDtos;

            myRoleDtos.Id = id;
            myRoleDtos.RoleName = myRoleDto.RoleName;
            myRoleDtos.TenantId = myRoleDto.TenantId;

            return myRoleDtos;
        }

        public async Task<PagedResultDto<MyRoleDto>> GetListAsync(MyRolePageDto input)
        {
            var roleParentId = _myRoleRepos.Where(x => x.TenantId == input.TenantID);
            var keyword = input.Keyword;
            var query = roleParentId.AsNoTracking()
                .WhereIf(
                             !string.IsNullOrEmpty(input.Keyword),
                             x => x.RoleName.Contains(keyword))
                ;
            var myRole = await query.Select
                (x => ObjectMapper.Map<MyRole, MyRoleDto>(x)).ToListAsync();
            return new PagedResultDto<MyRoleDto>
            {
                TotalCount = await query.CountAsync(),
                Items = myRole
            };
        }
        /*private readonly IRepository<Permission, Guid> _repository;
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

        *//*[HttpPost("TestPost")]*//*
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
        }*/

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
