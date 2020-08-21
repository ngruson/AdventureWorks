using MediatR;

namespace AW.Application.CountCustomers
{
    public class CountCustomersQuery : IRequest<int>
    {
        public string Territory { get; set; }
    }
}