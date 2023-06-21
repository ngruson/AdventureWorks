using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.CreateCustomerAddress
{
    public class CreateCustomerAddressCommandValidator : AbstractValidator<CreateCustomerAddressCommand>
    {
        public CreateCustomerAddressCommandValidator()
        {
            RuleFor(cmd => cmd.CustomerId)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerAddress.AddressType)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerAddress.Address!.AddressLine1)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerAddress.Address!.AddressLine2)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerAddress.Address!.PostalCode)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerAddress.Address!.City)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerAddress.Address!.StateProvinceCode)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerAddress.Address!.CountryRegionCode)
                .NotEmpty();
        }
    }
}
