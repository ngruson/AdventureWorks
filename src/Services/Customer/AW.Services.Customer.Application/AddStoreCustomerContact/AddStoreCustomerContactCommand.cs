﻿using MediatR;

namespace AW.Services.Customer.Application.AddStoreCustomerContact
{
    public class AddStoreCustomerContactCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public StoreCustomerContactDto CustomerContact { get; set; }
    }
}