using Dapper;
using Hieu.FinalProject.Invoice.InvoiceHeader.Dtos;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hieu.FinalProject.Invoice.InvoiceHeader.Repositories
{
    public class InvoiceHeaderDapperRepository: IInvoiceHeaderDapperRepository
    {
        private readonly string _connectionString;
        //private readonly IObjectMapper<FinalProjectApplicationModule> _objectMapper;
        public InvoiceHeaderDapperRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Default");
        }

        public async Task<List<InvoiceHeaderDto>> GetListAsync(InvoiceHeaderPageDto input)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (input.Keyword == null)
                {
                    var query = await connection.QueryAsync<InvoiceHeaderDto>("SELECT * FROM AppInvoiceHeaders");
                    return query.ToList();
                }
                else
                {
                    var query = await connection.QueryAsync<InvoiceHeaderDto>("SELECT * FROM AppInvoiceHeaders WHERE InvoiceNumber LIKE '%' + @InvoiceNumber +'%'", new { InvoiceNumber = input.Keyword });
                    return query.ToList();
                }

            }
            //var count = query.Count();
        }
    }
}
