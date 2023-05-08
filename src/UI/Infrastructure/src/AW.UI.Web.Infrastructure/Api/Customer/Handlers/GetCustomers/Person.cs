﻿using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomers
{
    public class Person
    {
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public List<PersonEmailAddress>? EmailAddresses { get; set; }
        public List<PersonPhone>? PhoneNumbers { get; set; }
    }
}