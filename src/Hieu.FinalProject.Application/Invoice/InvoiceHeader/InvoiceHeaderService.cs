using Hieu.FinalProject.EntityFrameworkCore;
using Hieu.FinalProject.Invoice.InvoiceDetail;
using Hieu.FinalProject.Invoice.InvoiceHeader.Dtos;
using Hieu.FinalProject.Invoice.InvoiceTaxBreak;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
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

        public async Task<InvoiceHeaderDto> UpdateAsync(long id, InvoiceHeaderDto input)
        {
            var invoiceHeaderID = await _repository.GetAsync(id);

            invoiceHeaderID.TaxCodeBuyer = input.TaxCodeBuyer;
            invoiceHeaderID.CompanyNameBuyer = input.CompanyNameBuyer;
            invoiceHeaderID.AddressBuyer = input.AddressBuyer;
            invoiceHeaderID.TaxCodeSeller = input.TaxCodeSeller;
            invoiceHeaderID.CustomerIdSeller = input.CustomerIdSeller;
            invoiceHeaderID.CompanyNameSeller = input.CompanyNameSeller;
            invoiceHeaderID.FulNameSeller = input.FulNameSeller;
            invoiceHeaderID.EmailSeller = input.EmailSeller;
            invoiceHeaderID.AddressSeller = input.AddressSeller;
            invoiceHeaderID.BankNumberSeller = input.BankNumberSeller;
            invoiceHeaderID.NameBankSeller = input.NameBankSeller;
            invoiceHeaderID.InvoiceNumber = input.InvoiceNumber;
            invoiceHeaderID.InvoiceForm = input.InvoiceForm;
            invoiceHeaderID.InvoiceSign = input.InvoiceSign;
            invoiceHeaderID.InvoiceDay = input.InvoiceDay;
            invoiceHeaderID.Payments = input.Payments;
            invoiceHeaderID.InvoiceNote = input.InvoiceNote;
            invoiceHeaderID.TotalDiscountBeforeTax = input.TotalDiscountBeforeTax;
            invoiceHeaderID.TotalDiscountAfterTax = input.TotalDiscountAfterTax;
            invoiceHeaderID.PercentDiscountAfterTax = input.PercentDiscountAfterTax;
            invoiceHeaderID.TotalProduct = input.TotalProduct;
            invoiceHeaderID.TotalTax = input.TotalTax;
            invoiceHeaderID.TotalPay = input.TotalPay;

            await _repository.UpdateAsync(invoiceHeaderID);

            //Update Detail
            if (!input.InvoiceDetails.Any())
            {
                /*throw new UserFriendlyException("Không được trống!");*/
            }

            foreach (var inputItem in input.InvoiceDetails)
            {
                if(inputItem.Id == 0)
                {
                    var invoiceDetail = new InvoiceDetailEntity
                    {
                        InvoiceId = inputItem.InvoiceId,
                        NameProduct = inputItem.NameProduct,
                        ProductId = inputItem.ProductId,
                        Content = inputItem.Content,
                        Unit = inputItem.Unit,
                        Quantity = inputItem.Quantity,
                        Price = inputItem.Price,
                        PercentTaxSell = inputItem.PercentTaxSell,
                        PercentDiscountBeforeTax = inputItem.PercentDiscountBeforeTax,
                        PercentMoney = inputItem.PercentMoney,
                        IntoMoney = inputItem.IntoMoney

                    };
                    await _invoiceDetailRepos.InsertAsync(invoiceDetail);
                }
                else if (inputItem.Id != 0) {
                    var invoiceDetailEntity = await _invoiceDetailRepos.FirstOrDefaultAsync(x => x.Id == inputItem.Id);
                    if (invoiceDetailEntity == null)
                    {
                        continue;
                        //nhảy ra rồi lặp lại.
                    }
                    invoiceDetailEntity.InvoiceId = inputItem.InvoiceId;
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
                }
                else
                {

                }
                /*var invoiceDetailEntity = await _invoiceDetailRepos.FirstOrDefaultAsync(x => x.Id == inputItem.Id);
                if (invoiceDetailEntity == null)
                {
                    continue;
                    //nhảy ra rồi lặp lại.
                }
                invoiceDetailEntity.InvoiceId = inputItem.InvoiceId;
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

                await _invoiceDetailRepos.UpdateAsync(invoiceDetailEntity);*/
            }


            return ObjectMapper.Map<InvoiceHeader, InvoiceHeaderDto>(invoiceHeaderID); //ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.
        }
        public async Task<InvoiceHeaderDto> DeleteAsync(long id)
        {
            var invoiceHeader = await _repository.GetAsync(id);
            await _repository.DeleteAsync(id);

            //Del Detail
            var queryDetail = from invoiceDetail in _invoiceDetailRepos
                        where invoiceDetail.InvoiceId == id
                        select invoiceDetail;

            await _invoiceDetailRepos.DeleteManyAsync(queryDetail);
            //Del TaxBreak

            var queryTaxBreak = from invoiceTaxBreak in _invoiceTaxBreakRepos
                              where invoiceTaxBreak.InvoiceId == id
                              select invoiceTaxBreak;
            await _invoiceTaxBreakRepos.DeleteManyAsync(queryTaxBreak);
            /*var query = _invoiceDetailRepos.Where(x => x.InvoiceId == id).Select(x => new{});*/
            /*await _invoiceDetailRepos.DeleteAsync(x=>x.InvoiceId == id);*/


            return ObjectMapper.Map<InvoiceHeader, InvoiceHeaderDto>(invoiceHeader);
        }

        public async Task<InvoiceHeaderDto> GetAsync(long id)
        {
            var query = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            return ObjectMapper.Map<InvoiceHeader, InvoiceHeaderDto>(query);
        }

        public async Task<PagedResultDto<InvoiceHeaderDto>> GetListAsync(InvoiceHeaderPageDto input)
        {
            var keyword = input.Keyword;
            var query = _repository.AsNoTracking()
                .WhereIf(
                             !string.IsNullOrEmpty(input.Keyword),
                             x => x.FulNameSeller.Contains(keyword)
                             || x.CompanyNameSeller.Contains(keyword)
                             || x.TaxCodeSeller.Contains(keyword))
                ;
            var invoiceHeader = await query.Select
                (x => ObjectMapper.Map<InvoiceHeader, InvoiceHeaderDto>(x)).ToListAsync();
            return new PagedResultDto<InvoiceHeaderDto>
            {
                TotalCount = await query.CountAsync(),
                Items = invoiceHeader
            };
        }
    }
}
