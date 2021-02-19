using MediatR;

namespace AW.Services.Customer.Application.AddIndividualCustomerPhone
{
    public class AddIndividualCustomerPhoneCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public PhoneDto Phone { get; set; }
    }
}