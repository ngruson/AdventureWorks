using MediatR;

namespace AW.Services.Customer.Application.DeleteIndividualCustomerEmailAddress
{
    public class DeleteIndividualCustomerEmailAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}