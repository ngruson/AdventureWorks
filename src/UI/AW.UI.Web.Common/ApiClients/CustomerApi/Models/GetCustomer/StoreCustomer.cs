﻿using AW.Common.Interfaces;
using System.Collections.Generic;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomer
{
    public class StoreCustomer : Customer
    {
        public string Name { get; set; }
        public override string CustomerName => Name;
        public string SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; } = new List<StoreCustomerContact>();
    }
}