using AW.Application.GetCustomers;
using MediatR;

namespace AW.Application.CountCustomers
{
    public class CountCustomersQuery : IRequest<int>
    {
        public CustomerType? CustomerType { get; set; }
        public string Territory { get; set; }
    }
}