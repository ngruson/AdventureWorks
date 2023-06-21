using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.CreateIndividualCustomerPhone
{
    public class CreateIndividualCustomerPhoneCommand : IRequest<Result>
    {
        public CreateIndividualCustomerPhoneCommand(Guid customerId, Phone phone)
        {
            CustomerId = customerId;
            Phone = phone;
        }

        public Guid CustomerId { get; set; }
        public Phone Phone { get; set; }
    }
}
