using AW.Services.Customer.Core.Handlers.GetCustomer;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public class GetCustomersDto
    {
        public GetCustomersDto(List<CustomerDto> customers, int totalCustomers)
        {
            Customers = customers;
            TotalCustomers = totalCustomers;
        }

        public List<CustomerDto> Customers { get; private init; }
        public int TotalCustomers { get; private init; }
    }
}