using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
    }
}