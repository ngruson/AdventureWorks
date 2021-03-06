﻿using AW.Services.Customer.Application.Common;
using System.Collections.Generic;

namespace AW.Services.Customer.Application.GetCustomers
{
    public class PersonDto : IMapFrom<Domain.Customer>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddressDto> EmailAddresses { get; set; }
        public List<PersonPhoneDto> PhoneNumbers { get; set; }
    }
}