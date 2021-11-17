using Dapper;
using Hieu.FinalProject.Permissions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hieu.FinalProject.Access.Repositories
{
    public class PermissionDapperRepository : IPermissionDapperRepository
    {
        private readonly string _connectionString;
        public PermissionDapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task CreateAsync(CreateUpdatePermissionDto permissionDto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                permissionDto.Id = Guid.NewGuid();

                string insertQuery = @"INSERT INTO [dbo].[AppPermissions]([Id], [Name], [UserPermission], [BranchPermission], [CustomerPermission], [PerPermission], [InvoicePermision]) VALUES (@Id, @Name, @UserPermission, @BranchPermission, @CustomerPermission, @PerPermission, @InvoicePermision)";

                await connection.ExecuteAsync(insertQuery, permissionDto);
            }
        }

        public async Task<List<Permission>> GetListAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = await connection.QueryAsync<Permission>("SELECT * FROM AppPermissions");
                return query.ToList();
            }
        }
    }
}
