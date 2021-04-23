using System.Collections.Generic;

namespace AW.Services.Customer.Application.AddCustomer
{
    public abstract class CustomerDto
    {
        public string AccountNumber { get; set; }
        public string Territory { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
    }
}