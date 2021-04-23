using MediatR;

namespace AW.Services.Customer.Application.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
    }
}