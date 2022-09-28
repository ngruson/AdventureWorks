using AW.Services.Customer.Core.Entities.PreferredAddress;
using AW.Services.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.Entities
{
    public abstract class Customer : IAggregateRoot
    {
        protected Customer() { }
        protected Customer(string accountNumber)
        {
            AccountNumber = accountNumber;
        }

        protected IPreferredAddressFactory preferredAddressFactory;

        public int Id { get; set; }
        public abstract CustomerType CustomerType { get; }
        public string AccountNumber { get; protected set; }
        public string Territory { get; private set; }

        public List<CustomerAddress> Addresses { get; internal set; } = new();

        public List<SalesOrder> SalesOrders { get; set; } = new();

        public Address GetPreferredAddress(string addressType)
        {
            return preferredAddressFactory.GetPreferredAddress(addressType);
        }

        public void AddAddress(CustomerAddress customerAddress)
        {
            Addresses.Add(customerAddress);
        }

        internal void RemoveAddress(CustomerAddress customerAddress)
        {
            Addresses.Remove(customerAddress);
        }
    }
}