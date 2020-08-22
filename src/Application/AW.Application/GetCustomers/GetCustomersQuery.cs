using MediatR;
using System.Collections.Generic;

namespace AW.Application.GetCustomers
{
    public class GetCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public CustomerType? CustomerType { get; set; }
        public string Territory { get; set; }
    }
}