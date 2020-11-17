using Ardalis.Specification;
using AW.Application.Specifications;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AW.Application.Customer.AddCustomerContactInfo
{
    public class AddCustomerContactInfoCommandValidator : AbstractValidator<AddCustomerContactInfoCommand>
    {
        private readonly IRepositoryBase<Domain.Sales.Customer> customerRepository;
        private readonly IRepositoryBase<Domain.Person.PhoneNumberType> phoneNumberTypeRepository;

        public AddCustomerContactInfoCommandValidator(
            IRepositoryBase<Domain.Sales.Customer> customerRepository,
            IRepositoryBase<Domain.Person.PhoneNumberType> phoneNumberTypeRepository
        )
        {
            this.customerRepository = customerRepository;
            this.phoneNumberTypeRepository = phoneNumberTypeRepository;

            RuleFor(cmd => cmd.AccountNumber)
                .NotEmpty().WithMessage("Account number is required")
                .MaximumLength(10).WithMessage("Account number must not exceed 10 characters")
                .MustAsync(CustomerExist).WithMessage("Customer does not exist");

            RuleFor(cmd => cmd.CustomerContactInfo)
                .NotNull().WithMessage("Customer contact info is required");

            RuleFor(cmd => cmd.CustomerContactInfo.Channel)
                .NotNull().WithMessage("Channel is required")
                .When(x => x.CustomerContactInfo != null);

            RuleFor(cmd => cmd.CustomerContactInfo.ContactInfoType)
                .NotEmpty().WithMessage("Contact info type is required")
                .MustAsync(ContactInfoTypeExist).WithMessage("Contact info type does not exist")
                .When(cmd => cmd.CustomerContactInfo != null && cmd.CustomerContactInfo.Channel == ContactInfoChannelTypeDto.Phone);

            RuleFor(cmd => cmd.CustomerContactInfo.Value)
                .NotEmpty().WithMessage("Value is required")
                .MaximumLength(25)
                .When(cmd => cmd.CustomerContactInfo != null && cmd.CustomerContactInfo.Channel == ContactInfoChannelTypeDto.Phone);

            RuleFor(cmd => cmd.CustomerContactInfo.Value)
                .NotEmpty().WithMessage("Value is required")
                .MaximumLength(50)
                .When(cmd => cmd.CustomerContactInfo != null && cmd.CustomerContactInfo.Channel == ContactInfoChannelTypeDto.Email);
        }

        private async Task<bool> CustomerExist(string accountNumber, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.GetBySpecAsync(new GetCustomerSpecification(accountNumber));
            return customer != null;
        }

        private async Task<bool> ContactInfoTypeExist(string name, CancellationToken cancellationToken)
        {
            var contactType = await phoneNumberTypeRepository.GetBySpecAsync(new GetPhoneNumberTypeSpecification(name));
            return contactType != null;
        }
    }
}