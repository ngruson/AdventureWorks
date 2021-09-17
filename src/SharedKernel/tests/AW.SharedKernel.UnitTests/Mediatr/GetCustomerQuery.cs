using MediatR;

namespace AW.SharedKernel.UnitTests.Mediatr
{
    public class GetCustomerQuery : IRequest<Customer>
    {
        public string CustomerNumber { get; set; }
    }
}