using MediatR;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.Handlers.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<List<CustomerDto>>
    {
        public CustomerType? CustomerType { get; set; }
    }
}