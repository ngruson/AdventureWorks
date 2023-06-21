using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.UpdateStoreCustomerContact
{
    public class UpdateStoreCustomerContactCommandValidator : AbstractValidator<UpdateStoreCustomerContactCommand>
    {
        public UpdateStoreCustomerContactCommandValidator()
        {
            RuleFor(_ => _.CustomerId)
                .NotEmpty();

            RuleFor(_ => _.CustomerContact!.ObjectId)
                .NotEmpty();

            RuleFor(_ => _.CustomerContact!.ContactType)
                .NotEmpty();

            RuleFor(_ => _.CustomerContact!.ContactPerson!.Name!.FirstName)
                .NotEmpty();

            RuleFor(_ => _.CustomerContact!.ContactPerson!.Name!.LastName)
                .NotEmpty();
        }
    }
}
