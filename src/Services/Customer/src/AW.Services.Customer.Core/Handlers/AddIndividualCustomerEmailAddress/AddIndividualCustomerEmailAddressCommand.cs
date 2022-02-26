﻿using AW.Services.SharedKernel.ValueObjects;
using MediatR;

namespace AW.Services.Customer.Core.Handlers.AddIndividualCustomerEmailAddress
{
    public class AddIndividualCustomerEmailAddressCommand : IRequest<Unit>
    {
        public string AccountNumber { get; set; }
        public EmailAddress EmailAddress { get; set; }
    }
}