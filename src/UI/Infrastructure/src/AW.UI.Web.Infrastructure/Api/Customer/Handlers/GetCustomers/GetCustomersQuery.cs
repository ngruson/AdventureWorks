using AW.SharedKernel.Interfaces;
using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers
{
    public class GetCustomersQuery : IRequest<GetCustomersResponse>
    {
        public GetCustomersQuery(int pageIndex, int pageSize, string? territory, CustomerType? customerType, string? accountNumber)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Territory = territory;
            CustomerType = customerType;
            AccountNumber = accountNumber;
        }

        public int PageIndex { get; init; }
        public int PageSize { get; init; }
        public string? Territory { get; init; }
        public CustomerType? CustomerType { get; init; }
        public string? AccountNumber { get; init; }
    }
}