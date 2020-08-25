using AW.Domain.Sales;
using MediatR;

namespace AW.Application.GetCustomers
{
    public class GetCustomersQuery : IRequest<GetCustomersDto>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public CustomerType? CustomerType { get; set; }
        public string Territory { get; set; }
    }
}