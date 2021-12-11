using Hieu.FinalProject.Invoice.InvoiceHeader.Dtos;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Hieu.FinalProject.Invoice.InvoiceHeader
{
    public class InvoiceHeaderService : ApplicationService
    {
        private readonly IRepository<InvoiceHeader, long> _repository;

        public InvoiceHeaderService(IRepository<InvoiceHeader, long> repository)
        {
            _repository = repository;
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
                TotalPay = input.TotalPay,
            };
            await _repository.InsertAsync(invoiceHeader);

            var idHeader = invoiceHeader.Id;

            return ObjectMapper.Map<InvoiceHeader, InvoiceHeaderDto>(invoiceHeader); //ObjectMapper này có sẵn từ lớp kế thừa nếu k có thì phải khai báo mới dùng đc.
        }
    }
}
