using FluentValidation;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomer
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(_ => _.ObjectId)
                .NotEmpty();
        }
    }
}
