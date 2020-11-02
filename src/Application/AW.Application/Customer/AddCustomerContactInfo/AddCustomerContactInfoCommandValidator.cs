using AW.Application.Interfaces;
using AW.Application.Specifications;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.AddCustomerContactInfo
{
    public class AddCustomerContactInfoCommandValidator : AbstractValidator<AddCustomerContactInfoCommand>
    {
        private readonly IAsyncRepository<Domain.Sales.Customer> customerRepository;
        private readonly IAsyncRepository<Domain.Person.PhoneNumberType> phoneNumberTypeRepository;

        public AddCustomerContactInfoCommandValidator(
            IAsyncRepository<Domain.Sales.Customer> customerRepository,
            IAsyncRepository<Domain.Person.PhoneNumberType> phoneNumberTypeRepository
        )
        {
            this.customerRepository = customerRepository;
            this.phoneNumberTypeRepository = phoneNumberTypeRepository;

            RuleFor(cmd => cmd.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(CustomerExist).WithMessage("Customer does not exist");

            RuleFor(cmd => cmd.CustomerContactInfo.Channel)
                .NotNull().WithMessage("Channel is required");

            When(cmd => cmd.CustomerContactInfo.Channel == ContactInfoChannelTypeDto.Phone, () =>
            {
                RuleFor(cmd => cmd.CustomerContactInfo.ContactInfoType)
                    .NotEmpty().WithMessage("Contact info type is required")
                    .MustAsync(ContactInfoTypeExist).WithMessage("Contact info type does not exist");

                RuleFor(cmd => cmd.CustomerContactInfo.Value)
                    .NotEmpty().WithMessage("Value is required")
                    .MaximumLength(25);
            })
            .Otherwise(() =>
            {
                RuleFor(cmd => cmd.CustomerContactInfo.Value)
                    .NotEmpty().WithMessage("Value is required")
                    .MaximumLength(50);
            });
        }

        private async Task<bool> CustomerExist(string accountNumber, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.FirstOrDefaultAsync(new GetCustomerSpecification(accountNumber));
            return customer != null;
        }

        private async Task<bool> ContactInfoTypeExist(string name, CancellationToken cancellationToken)
        {
            var contactType = await phoneNumberTypeRepository.FirstOrDefaultAsync(new GetPhoneNumberTypeSpecification(name));
            return contactType != null;
        }
    }
}