using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.CreateIndividualCustomerPhone
{
    public class CreateIndividualCustomerPhoneCommandValidator : AbstractValidator<CreateIndividualCustomerPhoneCommand>
    {
        public CreateIndividualCustomerPhoneCommandValidator()
        {
            RuleFor(cmd => cmd.CustomerId)
             .NotEmpty();

            RuleFor(cmd => cmd.Phone.PhoneNumberType)
             .NotEmpty();

            RuleFor(cmd => cmd.Phone.PhoneNumber)
             .NotEmpty();
        }
    }
}
