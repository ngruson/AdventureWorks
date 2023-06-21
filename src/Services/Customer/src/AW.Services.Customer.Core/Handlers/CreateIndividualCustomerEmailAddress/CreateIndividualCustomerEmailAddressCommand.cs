using Ardalis.Result;
using AW.Services.SharedKernel.ValueTypes;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.CreateIndividualCustomerEmailAddress
{
    public class CreateIndividualCustomerEmailAddressCommand : IRequest<Result>
    {
        public CreateIndividualCustomerEmailAddressCommand(Guid customerId, EmailAddress emailAddress)
        {
            CustomerId = customerId;
            EmailAddress = emailAddress;
        }

        public Guid CustomerId { get; private init; }
        public EmailAddress EmailAddress { get; private init; }
    }
}
