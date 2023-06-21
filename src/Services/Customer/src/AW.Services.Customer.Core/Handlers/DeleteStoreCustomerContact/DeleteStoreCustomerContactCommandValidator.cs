using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact
{
    public class DeleteStoreCustomerContactCommandValidator : AbstractValidator<DeleteStoreCustomerContactCommand>
    {
        public DeleteStoreCustomerContactCommandValidator()
        {
            RuleFor(_ => _.CustomerId)
                .NotEmpty();

            RuleFor(_ => _.ContactId)
                .NotEmpty();
        }
    }
}
