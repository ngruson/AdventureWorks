using AW.Services.Customer.Core.Entities.PreferredAddress;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.Entities
{
    public class StoreCustomer : Customer
    {
        public StoreCustomer()
        {
            preferredAddressFactory = new StorePreferredAddressFactory(this);
        }

        public StoreCustomer(string name, string accountNumber) : base()
        {
            Name = name;
            AccountNumber = accountNumber;
        }

        public override CustomerType CustomerType => CustomerType.Store;
        public string? Name { get; private set; }
        public string? SalesPerson { get; private set; }

        public List<StoreCustomerContact> Contacts { get; internal set; } = new();

        public void AddContact(StoreCustomerContact contact)
        {
            Contacts.Add(contact);
        }

        public void RemoveContact(StoreCustomerContact contact)
        {
            Contacts.Remove(contact);
        }
    }
}