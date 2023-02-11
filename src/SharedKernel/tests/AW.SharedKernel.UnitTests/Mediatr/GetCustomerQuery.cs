using MediatR;

namespace AW.SharedKernel.UnitTests.Mediatr
{
    public class GetCustomerQuery : IRequest<Customer>
    {
        public GetCustomerQuery(string customerNumber)
        {
            CustomerNumber = customerNumber;
        }

        public string CustomerNumber { get; private init; }
    }
}