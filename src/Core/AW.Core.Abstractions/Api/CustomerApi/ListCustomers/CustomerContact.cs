﻿namespace AW.Core.Abstractions.Api.CustomerApi.ListCustomers
{
    public class CustomerContact
    {
        public string ContactType { get; set; }
        public Contact Contact { get; set; }
        public string EmailAddress { get; set; }
    }
}