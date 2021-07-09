using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone
{
    public class DeleteIndividualCustomerPhoneCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public PhoneDto Phone { get; set; }
    }
}