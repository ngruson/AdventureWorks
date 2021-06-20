﻿using AW.Common.Extensions;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomer
{
    public class IndividualCustomer : Customer
    {
        public override string CustomerName => Person.FullName();
        public Person Person { get; set; }
    }
}