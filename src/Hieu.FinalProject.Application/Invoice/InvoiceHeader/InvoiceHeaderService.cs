using Hieu.FinalProject.EntityFrameworkCore;
using Hieu.FinalProject.Invoice.InvoiceDetail;
using Hieu.FinalProject.Invoice.InvoiceHeader.Dtos;
using Hieu.FinalProject.Invoice.InvoiceTaxBreak;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Hieu.FinalProject.Invoice.InvoiceHeader
{
    public class InvoiceHeaderService : ApplicationService
    {
        private readonly IRepository<InvoiceHeader, long> _repository;
        private readonly IRepository<InvoiceDetailEntity, long> _invoiceDetailRepos;
        private readonly IRepository<InvoiceTaxBreakEntity, long> _invoiceTaxBreakRepos;

        public InvoiceHeaderService(IRepository<InvoiceHeader, long> repository,
                                    IRepository<InvoiceDetailEntity, long> invoiceDetailRepos,
                                    IRepository<InvoiceTaxBreakEntity, long> invoiceTaxBreakRepos)
        {   
            _repository = repository;
            _invoiceDetailRepos = invoiceDetailRepos;
            _invoiceTaxBreakRepos = invoiceTaxBreakRepos;
        }

        public async Task<InvoiceHeaderDto> CreateAsync(InvoiceHeaderDto input)
        {
            var invoiceHeader = new InvoiceHeader
            {
                TaxCodeBuyer = input.TaxCodeBuyer,
                CompanyNameBuyer = input.CompanyNameBuyer,
                AddressBuyer = input.AddressBuyer,
                TaxCodeSeller = input.TaxCodeSeller,
                CustomerIdSeller = input.CustomerIdSeller,
                CompanyNameSeller = input.CompanyNameSeller,
                FulNameSeller = input.FulNameSeller,
                EmailSeller = input.EmailSeller,
                AddressSeller = input.AddressSeller,
                BankNumberSeller = input.BankNumberSeller,
                NameBankSeller = input.NameBankSeller,
                InvoiceNumber = input.InvoiceNumber,
                InvoiceForm = input.InvoiceForm,
                InvoiceSign = input.InvoiceSign,
                InvoiceDay = input.InvoiceDay,
                Payments = input.Payments,
                InvoiceNote = input.InvoiceNote,
                TotalDiscountBeforeTax = input.TotalDiscountBeforeTax,
                TotalDiscountAfterTax = input.TotalDiscountAfterTax,
                PercentDiscountAfterTax = input.PercentDiscountAfterTax,
                TotalProduct = input.TotalProduct,
                TotalTax = input.TotalTax,
                TotalPay = input.TotalPay
            };

            await _repository.InsertAsync(invoiceHeader);

            //lấy id của InvoiceHeader
            await UnitOfWorkManager.Current.SaveChangesAsync();

            var idHeader = invoiceHeader.Id;

            var invoiceDetails = new List<InvoiceDetailEntity>();
            if (!input.InvoiceDetails.Any())
            {
                // trả ra lỗi 
            }   
            
            foreach (var item in input.InvoiceDetails)
            {
                var invoiceDetail = new InvoiceDetailEntity
                {
                    InvoiceId = idHeader,
                    NameProduct = item.NameProduct,
                    ProductId = item.ProductId,
                    Content = item.Content,
                    Unit = item.Unit,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    PercentTaxSell = item.PercentTaxSell,
                    PercentDiscountBeforeTax = item.PercentDiscountBeforeTax,
                    PercentMoney = item.PercentMoney,
                    IntoMoney = item.IntoMoney

                };

                invoiceDetails.Add(invoiceDetail);
            }

            await _invoiceDetailRepos.InsertManyAsync(invoiceDetails);

            //TaxBreak
            var invoiceTaxBreaks = new List<InvoiceTaxBreakEntity>();
            if (!input.InvoiceTaxBreaks.Any())
            {
                // trả ra lỗi 
            }

            foreach (var item in input.InvoiceTaxBreaks)
            {
                var invoiceTaxBreak = new InvoiceTaxBreakEntity
                {
                    InvoiceId = idHeader,
                    NameTaxSell = item.NameTaxSell,
                    PercentTaxSell = item.PercentTaxSell,
                    MoneyTaxSell = item.MoneyTaxSell
                };

                invoiceTaxBreaks.Add(invoiceTaxBreak);
            }

            await _invoiceTaxBreakRepos.InsertManyAsync(invoiceTaxBreaks);


            return ObjectMapper.Map<InvoiceHeader, InvoiceHeaderDto>(invoiceHeader); //ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.
        }
    }
}
