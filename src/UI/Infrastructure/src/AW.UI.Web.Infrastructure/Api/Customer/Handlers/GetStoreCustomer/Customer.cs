﻿using AW.SharedKernel.Interfaces;
using System.Text.Json.Serialization;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetStoreCustomer;

public abstract class Customer : ICustomer
{
    public Guid ObjectId { get; set; }
    public CustomerType CustomerType { get; set; }
    public string? AccountNumber { get; set; }

    [JsonIgnore]
    public virtual string? CustomerName { get; }

    public string? Territory { get; set; }
    public List<CustomerAddress>? Addresses { get; set; }
    public List<SalesOrder>? SalesOrders { get; set; }
}
