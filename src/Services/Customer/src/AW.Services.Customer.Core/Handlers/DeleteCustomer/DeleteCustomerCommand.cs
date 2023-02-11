using MediatR;

namespace AW.Services.Customer.Core.Handlers.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public DeleteCustomerCommand(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public string AccountNumber { get; set; }
    }
}