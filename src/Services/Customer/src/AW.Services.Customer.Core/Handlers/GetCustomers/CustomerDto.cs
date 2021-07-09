﻿using System.Collections.Generic;

namespace AW.Services.Customer.Core.Handlers.GetCustomers
{
    public abstract class CustomerDto
    {
        public abstract CustomerType CustomerType { get; }
        public string AccountNumber { get; set; }
        public string Territory { get; set; }
        public List<CustomerAddressDto> Addresses { get; set; }
    }
}