using Hieu.FinalProject.EntityFrameworkCore;
using Hieu.FinalProject.Invoice.InvoiceDetail;
using Hieu.FinalProject.Invoice.InvoiceHeader.Dtos;
using Hieu.FinalProject.Invoice.InvoiceTaxBreak;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

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
                PersonCreateUpdate = input.PersonCreateUpdate,
                TenantId = input.TenantId,
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
            invoiceHeaderID.PersonCreateUpdate = input.PersonCreateUpdate;
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
                //thêm mới 
                if(inputItem.Id == 0)
                {
                    var invoiceDetail = new InvoiceDetailEntity
                    {
                        InvoiceId = id,
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
                //sửa
                else if (inputItem.Id != 0) {
                    var invoiceDetailEntity = await _invoiceDetailRepos.FirstOrDefaultAsync(x => x.Id == inputItem.Id);
                    if (invoiceDetailEntity == null)
                    {
                        continue;
                        //nhảy ra rồi lặp lại.
                    }
                    invoiceDetailEntity.InvoiceId = id;
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
            //xóa InvoiceDetail

            var LengthInvoiceDetailDto = 0;
            var invoiceDetailsDto = new List<InvoiceDetailDto>();
            foreach (var inputItem in input.InvoiceDetails)
            {
                //lấy ra những phần tử Dto sửa thôi.
                if(inputItem.Id != 0) {
                    LengthInvoiceDetailDto += 1;
                    invoiceDetailsDto.Add(inputItem);
                }
                
            }

            //await UnitOfWorkManager.Current.SaveChangesAsync();

            var invoiceDetailUpdates = from invoiceDetail in _invoiceDetailRepos
                              where invoiceDetail.InvoiceId == id
                              select invoiceDetail;
            var LengthInvoiceDetail = 0;

            foreach (var ItemEntity in invoiceDetailUpdates)
            {
                LengthInvoiceDetail += 1;
            }

            if(LengthInvoiceDetailDto < LengthInvoiceDetail)
            {
                var ItemMapper = ObjectMapper.Map<List<InvoiceDetailDto>, List<InvoiceDetailEntity>>(invoiceDetailsDto).Select(x=>x.Id);
                try
                {
                    var except = invoiceDetailUpdates.Where(x=>!ItemMapper.Contains(x.Id)).ToList();
                    await _invoiceDetailRepos.DeleteManyAsync(except);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                
            }

            //Update TaxBreak
            if (!input.InvoiceTaxBreaks.Any())
            {
                /*throw new UserFriendlyException("Không được trống!");*/
            }


            foreach (var inputItem in input.InvoiceTaxBreaks)
            {
                //thêm mới 
                if (inputItem.Id == 0)
                {
                    var invoiceTaxBreak = new InvoiceTaxBreakEntity
                    {
                        InvoiceId = id,
                        NameTaxSell = inputItem.NameTaxSell,
                        PercentTaxSell = inputItem.PercentTaxSell,
                        MoneyTaxSell = inputItem.MoneyTaxSell

                    };
                    await _invoiceTaxBreakRepos.InsertAsync(invoiceTaxBreak);
                }
                //sửa
                else if (inputItem.Id != 0)
                {
                    var invoiceTaxBreakEntity = await _invoiceTaxBreakRepos.FirstOrDefaultAsync(x => x.Id == inputItem.Id);
                    if (invoiceTaxBreakEntity == null)
                    {
                        continue;
                        //nhảy ra rồi lặp lại.
                    }
                    invoiceTaxBreakEntity.InvoiceId = id;
                    invoiceTaxBreakEntity.NameTaxSell = inputItem.NameTaxSell;
                    invoiceTaxBreakEntity.PercentTaxSell = inputItem.PercentTaxSell;
                    invoiceTaxBreakEntity.MoneyTaxSell = inputItem.MoneyTaxSell;

                    await _invoiceTaxBreakRepos.UpdateAsync(invoiceTaxBreakEntity);
                }

            }
            //xóa InvoiceTax

            var LengthInvoiceTaxBreakDto = 0;
            var invoiceTaxBreaksDto = new List<InvoiceTaxBreakDto>();
            foreach (var inputItem in input.InvoiceTaxBreaks)
            {
                //lấy ra những phần tử Dto sửa thôi.
                if (inputItem.Id != 0)
                {
                    LengthInvoiceTaxBreakDto += 1;
                    invoiceTaxBreaksDto.Add(inputItem);
                }

            }

            //await UnitOfWorkManager.Current.SaveChangesAsync();

            var invoiceTaxBreakUpdates = from invoiceTaxBreak in _invoiceTaxBreakRepos
                                         where invoiceTaxBreak.InvoiceId == id
                                         select invoiceTaxBreak;
            var LengthInvoiceTaxBreak = 0;

            foreach (var ItemEntity in invoiceTaxBreakUpdates)
            {
                LengthInvoiceTaxBreak += 1;
            }

            if (LengthInvoiceTaxBreakDto < LengthInvoiceTaxBreak)
            {
                var ItemMapper = ObjectMapper.Map<List<InvoiceTaxBreakDto>, List<InvoiceTaxBreakEntity>>(invoiceTaxBreaksDto).Select(x => x.Id);
                try
                {
                    var except = invoiceTaxBreakUpdates.Where(x => !ItemMapper.Contains(x.Id)).ToList();
                    await _invoiceTaxBreakRepos.DeleteManyAsync(except);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

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
            var invoiceHeaderDto = ObjectMapper.Map<InvoiceHeader, InvoiceHeaderDto>(query);

            var invoiceDetailsDtos = new List<InvoiceDetailDto>();
            invoiceDetailsDtos = await _invoiceDetailRepos.Where(x => x.InvoiceId == id).Select(x => ObjectMapper.Map<InvoiceDetailEntity, InvoiceDetailDto>(x)).ToListAsync();
            var invoiceTaxBreakDtos = new List<InvoiceTaxBreakDto>();
            invoiceTaxBreakDtos = await _invoiceTaxBreakRepos.Where(x => x.InvoiceId == id).Select(x => ObjectMapper.Map<InvoiceTaxBreakEntity, InvoiceTaxBreakDto>(x)).ToListAsync();

            var invoiceHeaderDtos = new InvoiceHeaderDto();
            invoiceHeaderDtos.InvoiceDetails = invoiceDetailsDtos;
            invoiceHeaderDtos.InvoiceTaxBreaks = invoiceTaxBreakDtos;

            invoiceHeaderDtos.Id = id;
            invoiceHeaderDtos.TenantId = invoiceHeaderDto.TenantId;
            invoiceHeaderDtos.PersonCreateUpdate = invoiceHeaderDto.PersonCreateUpdate;
            invoiceHeaderDtos.TaxCodeBuyer = invoiceHeaderDto.TaxCodeBuyer;
            invoiceHeaderDtos.CompanyNameBuyer = invoiceHeaderDto.CompanyNameBuyer;
            invoiceHeaderDtos.AddressBuyer = invoiceHeaderDto.AddressBuyer;
            invoiceHeaderDtos.TaxCodeSeller = invoiceHeaderDto.TaxCodeSeller;
            invoiceHeaderDtos.CustomerIdSeller = invoiceHeaderDto.CustomerIdSeller;
            invoiceHeaderDtos.CompanyNameSeller = invoiceHeaderDto.CompanyNameSeller;
            invoiceHeaderDtos.FulNameSeller = invoiceHeaderDto.FulNameSeller;
            invoiceHeaderDtos.EmailSeller = invoiceHeaderDto.EmailSeller;
            invoiceHeaderDtos.AddressSeller = invoiceHeaderDto.AddressSeller;
            invoiceHeaderDtos.BankNumberSeller = invoiceHeaderDto.BankNumberSeller;
            invoiceHeaderDtos.NameBankSeller = invoiceHeaderDto.NameBankSeller;
            invoiceHeaderDtos.InvoiceNumber = invoiceHeaderDto.InvoiceNumber;
            invoiceHeaderDtos.InvoiceForm = invoiceHeaderDto.InvoiceForm;
            invoiceHeaderDtos.InvoiceSign = invoiceHeaderDto.InvoiceSign;
            invoiceHeaderDtos.InvoiceDay = invoiceHeaderDto.InvoiceDay;
            invoiceHeaderDtos.Payments = invoiceHeaderDto.Payments;
            invoiceHeaderDtos.InvoiceNote = invoiceHeaderDto.InvoiceNote;
            invoiceHeaderDtos.TotalDiscountBeforeTax = invoiceHeaderDto.TotalDiscountBeforeTax;
            invoiceHeaderDtos.TotalDiscountAfterTax = invoiceHeaderDto.TotalDiscountAfterTax;
            invoiceHeaderDtos.PercentDiscountAfterTax = invoiceHeaderDto.PercentDiscountAfterTax;
            invoiceHeaderDtos.TotalProduct = invoiceHeaderDto.TotalProduct;
            invoiceHeaderDtos.TotalTax = invoiceHeaderDto.TotalTax;
            invoiceHeaderDtos.TotalPay = invoiceHeaderDto.TotalPay;

            return invoiceHeaderDtos;
        }

        public async Task<PagedResultDto<InvoiceHeaderDto>> GetListAsync(InvoiceHeaderPageDto input)
        {
            var invoiceParentId = _repository.Where(x => x.TenantId == input.TenantID);
            var keyword = input.Keyword;
            var query = invoiceParentId.AsNoTracking()
                .WhereIf(
                             !string.IsNullOrEmpty(input.Keyword),
                             x => x.InvoiceNumber.ToString().Contains(keyword)
                             || x.CompanyNameSeller.Contains(keyword)
                             || x.TaxCodeSeller.Contains(keyword))
                .OrderByDescending(x => x.Id)
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
