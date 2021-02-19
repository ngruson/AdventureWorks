using MediatR;

namespace AW.Services.Customer.Application.AddIndividualCustomerEmailAddress
{
    public class AddIndividualCustomerEmailAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}