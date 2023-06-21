using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress
{
    public class DeleteIndividualCustomerEmailAddressCommandValidator : AbstractValidator<DeleteIndividualCustomerEmailAddressCommand>
    {
        public DeleteIndividualCustomerEmailAddressCommandValidator()
        {
            RuleFor(_ => _.CustomerId)
                .NotEmpty();

            RuleFor(_ => _.EmailAddressId)
                .NotEmpty();
        }
    }
}
