using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.CreateStoreCustomerContact
{
    public class CreateStoreCustomerContactCommandValidator : AbstractValidator<CreateStoreCustomerContactCommand>
    {
        public CreateStoreCustomerContactCommandValidator()
        {
            RuleFor(cmd => cmd.CustomerId)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerContact.ContactType)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerContact.ContactPerson!.Name!.FirstName)
                .NotEmpty();

            RuleFor(cmd => cmd.CustomerContact.ContactPerson!.Name!.LastName)
                .NotEmpty();
        }
    }
}
