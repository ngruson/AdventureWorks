﻿namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetCustomer;

public class StoreCustomerContact
{
    public Guid ObjectId { get; set; }

    public string? ContactType { get; set; }
    public Person? ContactPerson { get; set; }
}
