using AW.Services.Infrastructure;
using System;
using System.Runtime.Serialization;

namespace AW.Services.Customer.Core.Exceptions
{
    [Serializable]
    public class CustomerNotFoundException : DomainException, ISerializable
    {
        public CustomerNotFoundException(string accountNumber)
            : base($"Customer {accountNumber} not found")
        { }
    }
}