﻿using Hieu.FinalProject.Customers.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Hieu.FinalProject.Customers
{
    public class CustomerService : CrudAppService<
        Customer,
        CustomerDto,
        long,
        CustomerPageDto,
        CreateUpdateCustomerDto>,
        ICustomerService
    {
        private readonly IRepository<Customer, long> _repository;

        public CustomerService(IRepository<Customer, long> repository) : base(repository)
        {
            _repository = repository;

        }

        public override Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input)
        {
            return base.CreateAsync(input);
        }

        public override Task DeleteAsync(long id)
        {
            return base.DeleteAsync(id);
        }

        public override Task<CustomerDto> GetAsync(long id)
        {
            return base.GetAsync(id);
        }

        public override async Task<PagedResultDto<CustomerDto>> GetListAsync(CustomerPageDto input)
        {
            var keyword = input.Keyword;
            var query = _repository.AsNoTracking()
                .WhereIf(
                             !string.IsNullOrEmpty(input.Keyword),
                             x => x.Name.Contains(keyword)
                             || x.Address.Contains(keyword)
                             || x.Daidienphapnhan.Contains(keyword))
                ;
            var currencies = await query.Select
                (x => ObjectMapper.Map<Customer, CustomerDto>(x)).PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();
            return new PagedResultDto<CustomerDto>
            {
                TotalCount = await query.CountAsync(),
                Items = currencies
            };
        }

        public override Task<CustomerDto> UpdateAsync(long id, CreateUpdateCustomerDto input)
        {
            return base.UpdateAsync(id, input);
        }
    }
}
