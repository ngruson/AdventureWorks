﻿using MediatR;

namespace AW.Core.Application.Customer.AddCustomerAddress
{
    public class AddCustomerAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerAddressDto CustomerAddress { get; set; }
    }
}