using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerEmailAddress
{
    public class DeleteIndividualCustomerEmailAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}