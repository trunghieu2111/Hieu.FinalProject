using AutoMapper;
using Dapper;
using Hieu.FinalProject.Permissions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace Hieu.FinalProject.Access.Repositories
{
    public class PermissionDapperRepository : IPermissionDapperRepository
    {
        private readonly string _connectionString;
        //private readonly IObjectMapper<FinalProjectApplicationModule> _objectMapper;
        public PermissionDapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<PermissionDto> CreateAsync(CreateUpdatePermissionDto permissionDto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                permissionDto.Id = Guid.NewGuid();

                //vì khi thêm k thêm trường Id nên DTO chỉ hiểu là kiểu GUID chứ không tự sinh ra được ID, phải gán ID.

                string insertQuery = @"INSERT INTO AppPermissions ([Id], [Name], [UserPermission], [BranchPermission], [CustomerPermission], [PerPermission], [InvoicePermision]) VALUES (@Id, @Name, @UserPermission, @BranchPermission, @CustomerPermission, @PerPermission, @InvoicePermision)";

                await connection.ExecuteAsync(insertQuery, permissionDto);

                return new PermissionDto {
                    Id = permissionDto.Id,
                    Name = permissionDto.Name,
                    UserPermission = permissionDto.UserPermission,
                    BranchPermission= permissionDto.BranchPermission,
                    CustomerPermission = permissionDto.CustomerPermission,
                    PerPermission = permissionDto.PerPermission,
                    InvoicePermision= permissionDto.InvoicePermision
                };
            }
        }

        public async Task<List<PermissionDto>> GetListAsync(PermissionPageDto input)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if(input.Keyword == null)
                {
                    var query = await connection.QueryAsync<PermissionDto>("SELECT * FROM AppPermissions");
                    return query.ToList();
                }
                else
                {
                    var query = await connection.QueryAsync<PermissionDto>("SELECT * FROM AppPermissions WHERE Name LIKE '%' + @NAME +'%' ", new { NAME = input.Keyword });
                    return query.ToList();
                }
                
            }
            /*var count = query.Count();*/
        }

        public async Task<PermissionDto> GetAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = await connection.QueryAsync<PermissionDto>("SELECT * FROM AppPermissions WHERE Id = @ID", new { ID = id });
                return query.SingleOrDefault();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string insertQuery = @"Delete AppPermissions WHERE Id = @ID";

                await connection.ExecuteAsync(insertQuery, new { ID = id });
            }
        }

        public async Task UpdateAsync(Guid id, CreateUpdatePermissionDto permissionDto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                /*var query = await connection.QueryAsync<CreateUpdatePermissionDto>("SELECT * FROM AppPermissions WHERE Id = @ID", new { ID = id });
                var permisstion = query.SingleOrDefault();*/
                string insertQuery = @"UPDATE AppPermissions SET Name = @Name, UserPermission = @UserPermission, BranchPermission = @BranchPermission, CustomerPermission = @CustomerPermission, PerPermission = @PerPermission, InvoicePermision = @InvoicePermision WHERE Id = @ID";

                await connection.ExecuteAsync(insertQuery, new { ID = id, Name = permissionDto.Name, UserPermission = permissionDto.UserPermission, BranchPermission = permissionDto.BranchPermission, CustomerPermission =permissionDto.CustomerPermission, PerPermission =permissionDto.PerPermission, InvoicePermision =permissionDto.InvoicePermision});
            }
        }
    }
}
