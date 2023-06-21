using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress
{
    public class DeleteIndividualCustomerEmailAddressCommand : IRequest<Result>
    {
        public DeleteIndividualCustomerEmailAddressCommand(Guid customerId, Guid emailAddressId)
        {
            CustomerId = customerId;
            EmailAddressId = emailAddressId;
        }

        public Guid CustomerId { get; private init; }
        public Guid EmailAddressId { get; private init; }
    }
}
