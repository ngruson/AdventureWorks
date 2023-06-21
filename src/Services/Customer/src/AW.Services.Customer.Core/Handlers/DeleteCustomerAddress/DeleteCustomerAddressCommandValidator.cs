using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomerAddress
{
    public class DeleteCustomerAddressCommandValidator : AbstractValidator<DeleteCustomerAddressCommand>
    {
        public DeleteCustomerAddressCommandValidator()
        {
            RuleFor(_ => _.CustomerId)
                .NotEmpty();

            RuleFor(_ => _.AddressId)
                .NotEmpty();
        }
    }
}
