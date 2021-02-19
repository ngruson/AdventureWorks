using AW.Services.Customer.Application.Common;
using System.Collections.Generic;

namespace AW.Services.Customer.Application.GetCustomer
{
    public abstract class CustomerDto : IMapFrom<Domain.Customer>
    {
        public string AccountNumber { get; set; }
        public string TerritoryName { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
    }
}