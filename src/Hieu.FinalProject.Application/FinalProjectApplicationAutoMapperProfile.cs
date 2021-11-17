﻿using AutoMapper;
using Hieu.FinalProject.Access;
using Hieu.FinalProject.Accounts;
using Hieu.FinalProject.Accounts.Dtos;
using Hieu.FinalProject.Branchs;
using Hieu.FinalProject.Customers;
using Hieu.FinalProject.Customers.Dtos;
using Hieu.FinalProject.Permissions;

namespace Hieu.FinalProject
{
    public class FinalProjectApplicationAutoMapperProfile : Profile
    {
        public FinalProjectApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Branch, BranchDto>().ReverseMap();
            CreateMap<CreateUpdateBranchDto, Branch>().ReverseMap();

            CreateMap<Permission, PermissionDto>().ReverseMap();
            CreateMap<CreateUpdatePermissionDto, Permission>().ReverseMap();

            CreateMap<Account, AccountDto>().ReverseMap();
            //ReverseMap 2 chiều
            CreateMap<CreateUpdateAccountDto, Account>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            //ReverseMap 2 chiều
            CreateMap<CreateUpdateCustomerDto, Customer>().ReverseMap();
        }
    }
}