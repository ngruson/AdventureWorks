using AW.Services.SharedKernel.ValueTypes;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress
{
    public class DeleteIndividualCustomerEmailAddressCommand : IRequest<Unit>
    {
        public DeleteIndividualCustomerEmailAddressCommand(string accountNumber, EmailAddress emailAddress)
        {
            AccountNumber = accountNumber;
            EmailAddress = emailAddress;
        }

        public string AccountNumber { get; private init; }
        public EmailAddress EmailAddress { get; private init; }
    }
}