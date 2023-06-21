using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteIndividualCustomerPhone
{
    public class DeleteIndividualCustomerPhoneCommand : IRequest<Result>
    {
        public DeleteIndividualCustomerPhoneCommand(Guid customerId, Guid phoneId)
        {
            CustomerId = customerId;
            PhoneId = phoneId;
        }

        public Guid CustomerId { get; set; }
        public Guid PhoneId { get; set; }
    }
}
