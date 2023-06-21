using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone
{
    public class DeleteIndividualCustomerPhoneCommandValidator : AbstractValidator<DeleteIndividualCustomerPhoneCommand>
    {
        public DeleteIndividualCustomerPhoneCommandValidator()
        {
            RuleFor(_ => _.CustomerId)
                .NotEmpty();

            RuleFor(_ => _.PhoneId)
                .NotEmpty();
        }
    }
}
