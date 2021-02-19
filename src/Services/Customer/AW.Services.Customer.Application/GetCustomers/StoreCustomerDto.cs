using AW.Services.Customer.Application.Common;
using System.Collections.Generic;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class StoreCustomerDto : CustomerDto, IMapFrom<Domain.StoreCustomer>
    {
        public string Name { get; set; }
        public string SalesPersonName { get; set; }
        public List<StoreCustomerContactDto> Contacts { get; set; } = new List<StoreCustomerContactDto>();
    }
}