using AW.Core.Domain.Sales;

namespace AW.Core.Abstractions.Api.CustomerApi.ListCustomers
{
    public class ListCustomersRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public CustomerType? CustomerType { get; set; }
        public string Territory { get; set; }
    }
}