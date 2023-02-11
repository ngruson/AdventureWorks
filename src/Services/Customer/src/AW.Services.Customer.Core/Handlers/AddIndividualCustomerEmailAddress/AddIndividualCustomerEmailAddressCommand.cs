using AW.Services.SharedKernel.ValueTypes;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.AddIndividualCustomerEmailAddress
{
    public class AddIndividualCustomerEmailAddressCommand : IRequest<Unit>
    {
        public AddIndividualCustomerEmailAddressCommand(string accountNumber, EmailAddress emailAddress)
        {
            AccountNumber = accountNumber;
            EmailAddress = emailAddress;
        }

        public string AccountNumber { get; private init; }
        public EmailAddress EmailAddress { get; private init; }
    }
}