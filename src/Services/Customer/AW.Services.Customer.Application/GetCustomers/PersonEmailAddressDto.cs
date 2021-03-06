﻿using AW.Services.Customer.Application.Common;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class PersonEmailAddressDto : IMapFrom<Domain.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }
    }
}