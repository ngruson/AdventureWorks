using Ardalis.Result;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Result>
    {
        public DeleteCustomerCommand()
        {
        }
        public DeleteCustomerCommand(Guid objectId)
        {
            ObjectId = objectId;
        }

        public Guid ObjectId { get; set; }
    }
}
