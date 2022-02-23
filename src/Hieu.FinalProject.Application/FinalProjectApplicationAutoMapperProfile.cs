using AutoMapper;
using Hieu.FinalProject.Access.Dtos;
using Hieu.FinalProject.Accounts;
using Hieu.FinalProject.Accounts.Dtos;
using Hieu.FinalProject.Branchs;
using Hieu.FinalProject.Customers;
using Hieu.FinalProject.Customers.Dtos;
using Hieu.FinalProject.Invoice.InvoiceDetail;
using Hieu.FinalProject.Invoice.InvoiceHeader;
using Hieu.FinalProject.Invoice.InvoiceHeader.Dtos;
using Hieu.FinalProject.Invoice.InvoiceTaxBreak;
using Hieu.FinalProject.Permission_Role;
using Hieu.FinalProject.Role;

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

            CreateMap<MyRole, MyRoleDto>().ReverseMap();
            CreateMap<MyPermission_Role, MyPermissionRoleDto>().ReverseMap();

            CreateMap<Account, AccountDto>().ReverseMap();
            //ReverseMap 2 chiều
            CreateMap<CreateUpdateAccountDto, Account>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            //ReverseMap 2 chiều
            CreateMap<CreateUpdateCustomerDto, Customer>().ReverseMap();

            CreateMap<InvoiceDetailEntity, InvoiceDetailDto>().ReverseMap();
            //CreateMap<CreateUpdateInvoiceDetailDto, InvoiceDetail>().ReverseMap();

            CreateMap<InvoiceHeader, InvoiceHeaderDto>().ReverseMap();
            //ReverseMap 2 chiều
            //CreateMap<CreateUpdateInvoiceHeaderDto, InvoiceHeader>().ReverseMap();

            CreateMap<InvoiceTaxBreakEntity, InvoiceTaxBreakDto>().ReverseMap();
            //CreateMap<CreateUpdateInvoiceTaxBreakDto, InvoiceTaxBreak>().ReverseMap();
        }
    }
}
