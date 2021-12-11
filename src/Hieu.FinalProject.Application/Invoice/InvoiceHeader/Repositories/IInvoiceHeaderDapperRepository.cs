using Hieu.FinalProject.Invoice.InvoiceHeader.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hieu.FinalProject.Invoice.InvoiceHeader.Repositories
{
    public interface IInvoiceHeaderDapperRepository
    {
        Task<List<InvoiceHeaderDto>> GetListAsync(InvoiceHeaderPageDto input);
        //Task<InvoiceHeaderDto> CreateAsync(InvoiceHeaderDto permissionDto);
    }
}
