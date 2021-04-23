using AW.Services.Customer.Application.GetCustomer;
using System.Collections.Generic;

namespace AW.Services.Customer.WCF.Messages.GetCustomer
{
    public abstract class Customer
    {
        public string AccountNumber { get; set; }
        public string Territory { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
    }
}