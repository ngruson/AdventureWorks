using MediatR;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class GetCustomersQuery : IRequest<GetCustomersDto>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public CustomerType? CustomerType { get; set; }
        public string Territory { get; set; }
        public string AccountNumber { get; set; }
    }
}