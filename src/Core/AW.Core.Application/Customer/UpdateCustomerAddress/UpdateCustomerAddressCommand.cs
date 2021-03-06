﻿using MediatR;

namespace AW.Core.Application.Customer.UpdateCustomerAddress
{
    public class UpdateCustomerAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public CustomerAddressDto CustomerAddress { get; set; }
    }
}