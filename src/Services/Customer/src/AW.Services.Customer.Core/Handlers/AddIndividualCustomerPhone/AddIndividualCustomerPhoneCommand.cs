using MediatR;

namespace AW.Services.Customer.Core.Handlers.AddIndividualCustomerPhone
{
    public class AddIndividualCustomerPhoneCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public PhoneDto Phone { get; set; }
    }
}