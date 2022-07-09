using AW.Services.Customer.Core.Entities.PreferredAddress;
using System.Collections.Generic;
using System.Linq;

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
        public string Name { get; private set; }
        public string SalesPerson { get; private set; }

        public IEnumerable<StoreCustomerContact> Contacts => _contacts.ToList();
        private readonly List<StoreCustomerContact> _contacts = new();

        public void AddContact(StoreCustomerContact contact)
        {
            _contacts.Add(contact);
        }

        public void RemoveContact(StoreCustomerContact contact)
        {
            _contacts.Remove(contact);
        }
    }
}