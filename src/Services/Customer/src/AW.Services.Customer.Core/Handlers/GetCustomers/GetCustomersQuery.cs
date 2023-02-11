using AW.Services.Customer.Core.Handlers.GetCustomer;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class GetCustomersQuery : IRequest<GetCustomersDto?>
    {
        public GetCustomersQuery(int pageIndex, int pageSize, CustomerType? customerType, string territory, string accountNumber)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            CustomerType = customerType;
            Territory = territory;
            AccountNumber = accountNumber;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public CustomerType? CustomerType { get; set; }
        public string Territory { get; set; }
        public string AccountNumber { get; set; }
    }
}