using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Hieu.FinalProject.Customers.Dtos
{
    public interface ICustomerService : ICrudAppService<CustomerDto, long,
        CustomerPageDto, CreateUpdateCustomerDto>
    {
    }
}   
