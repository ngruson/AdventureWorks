﻿using AW.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.Entities
{
    public abstract class Customer : IAggregateRoot
    {
        public int Id { get; set; }
        public abstract CustomerType CustomerType { get; }
        public string AccountNumber { get; set; }
        public string Territory { get; set; }
        public List<CustomerAddress> Addresses { get; set; } = new List<CustomerAddress>();
        public List<SalesOrder> SalesOrders { get; set; } = new List<SalesOrder>();
    }
}