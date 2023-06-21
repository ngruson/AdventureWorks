using AW.Services.Customer.Core.Specifications;
using AW.Services.SharedKernel.Interfaces;
using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomerAddress;

public class UpdateCustomerAddressCommandValidator : AbstractValidator<UpdateCustomerAddressCommand>
{
    private readonly IRepository<Entities.Customer> customerRepository;

    public UpdateCustomerAddressCommandValidator(
        IRepository<Entities.Customer> customerRepository
    )
    {
        this.customerRepository = customerRepository;

        RuleFor(cmd => cmd.CustomerId)
            .NotEmpty()
            .MustAsync(CustomerExist).WithMessage("Customer does not exist");

        RuleFor(cmd => cmd.CustomerAddress)
            .NotNull().WithMessage("Customer address is required");

        RuleFor(cmd => cmd.CustomerAddress!.AddressType)
            .NotEmpty().WithMessage("Address type is required")
            .When(cmd => cmd.CustomerAddress != null);

        RuleFor(cmd => cmd.CustomerAddress!.Address)
            .NotNull().WithMessage("Address is required")
            .When(cmd => cmd.CustomerAddress != null);

        RuleFor(cmd => cmd.CustomerAddress!.Address!.AddressLine1)
            .NotEmpty().WithMessage("Address line 1 is required")
            .MaximumLength(60).WithMessage("Address line 1 must not exceed 60 characters")
            .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

        RuleFor(cmd => cmd.CustomerAddress!.Address!.AddressLine2)
            .MaximumLength(60).WithMessage("Address line 2 must not exceed 60 characters")
            .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

        RuleFor(cmd => cmd.CustomerAddress!.Address!.PostalCode)
            .NotEmpty().WithMessage("Postal code is required")
            .MaximumLength(15).WithMessage("Postal code must not exceed 15 characters")
            .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

        RuleFor(cmd => cmd.CustomerAddress!.Address!.City)
            .NotEmpty().WithMessage("City is required")
            .MaximumLength(30).WithMessage("City must not exceed 30 characters")
            .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

        RuleFor(cmd => cmd.CustomerAddress!.Address!.StateProvinceCode)
            .NotEmpty().WithMessage("State/province is required")
            .MaximumLength(3).WithMessage("State/province must not exceed 3 characters")
            .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

        RuleFor(cmd => cmd.CustomerAddress!.Address!.CountryRegionCode)
            .NotEmpty().WithMessage("Country is required")
            .MaximumLength(3).WithMessage("Country must not exceed 3 characters")
            .When(cmd => cmd.CustomerAddress != null && cmd.CustomerAddress.Address != null);

        RuleFor(cmd => cmd)
            .MustAsync(UniqueAddress).WithMessage("Address must be unique")
            .When(cmd => cmd.CustomerAddress != null);
    }

    private async Task<bool> UniqueAddress(UpdateCustomerAddressCommand command, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.SingleOrDefaultAsync(
            new GetCustomerSpecification(command.CustomerId),
            cancellationToken
        );

        if (customer == null)
            return true;

        var address = customer.Addresses.Find(a =>
            a.AddressType == command.CustomerAddress?.AddressType &&
            a.Address?.AddressLine1 == command.CustomerAddress?.Address?.AddressLine1 &&
            a.Address?.AddressLine2 == command.CustomerAddress?.Address?.AddressLine2 &&
            a.Address?.PostalCode == command.CustomerAddress?.Address?.PostalCode &&
            a.Address?.City == command.CustomerAddress?.Address?.City &&
            a.Address?.StateProvinceCode == command.CustomerAddress?.Address?.StateProvinceCode &&
            a.Address?.CountryRegionCode == command.CustomerAddress?.Address?.CountryRegionCode
        );

        return address == null;
    }

    private async Task<bool> CustomerExist(Guid objectId, CancellationToken cancellationToken)
    {
        return await customerRepository.AnyAsync(
            new GetCustomerSpecification(objectId),
            cancellationToken
        );
    }
}
