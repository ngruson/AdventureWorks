using AW.Services.SharedKernel.ValueObjects;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress
{
    public class DeleteIndividualCustomerEmailAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public EmailAddress EmailAddress { get; set; }
    }
}