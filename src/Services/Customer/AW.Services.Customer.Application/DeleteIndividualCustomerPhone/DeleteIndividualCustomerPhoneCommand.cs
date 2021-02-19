using MediatR;

namespace AW.Services.Customer.Application.DeleteIndividualCustomerPhone
{
    public class DeleteIndividualCustomerPhoneCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public PhoneDto Phone { get; set; }
    }
}