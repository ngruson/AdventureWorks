using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.CreateIndividualCustomerEmailAddress
{
    public class CreateIndividualCustomerEmailAddressCommandValidator : AbstractValidator<CreateIndividualCustomerEmailAddressCommand>
    {
        public CreateIndividualCustomerEmailAddressCommandValidator()
        {
            RuleFor(cmd => cmd.CustomerId)
                .NotEmpty();

            RuleFor(cmd => cmd.EmailAddress)
                .NotNull();

            RuleFor(cmd => cmd.EmailAddress.Value)
                .NotEmpty();
        }
    }
}
