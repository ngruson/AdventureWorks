﻿using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.GetCustomer
{
    public class StoreCustomerContactDto : IMapFrom<Domain.StoreCustomerContact>
    {
        public string ContactType { get; set; }
        public PersonDto ContactPerson { get; set; }
    }
}