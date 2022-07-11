using AW.Services.Customer.Core.Entities.PreferredAddress;
using AW.Services.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AW.Services.Customer.Core.Entities
{
    public abstract class Customer : IAggregateRoot
    {
        protected IPreferredAddressFactory preferredAddressFactory;

        public int Id { get; set; }
        public abstract CustomerType CustomerType { get; }
        public string AccountNumber { get; protected set; }
        public string Territory { get; private set; }

        public IEnumerable<CustomerAddress> Addresses => _addresses;
        private readonly List<CustomerAddress> _addresses = new();

        public IEnumerable<SalesOrder> SalesOrders => _salesOrders;
        private readonly List<SalesOrder> _salesOrders = new();

        public Address GetPreferredAddress(string addressType)
        {
            return preferredAddressFactory.GetPreferredAddress(addressType);
        }

        public void AddAddress(CustomerAddress customerAddress)
        {
            _addresses.Add(customerAddress);
        }

        internal void RemoveAddress(CustomerAddress customerAddress)
        {
            _addresses.Remove(customerAddress);
        }
    }
}