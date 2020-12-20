using AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using System.Linq;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer
{
    public class StoreBuilder
    {
        private Store store = new Store();

        public StoreBuilder Name(string name)
        {
            store.Name = name;
            return this;
        }

        public StoreBuilder SalesPerson(SalesPerson salesPerson)
        {
            store.SalesPerson = salesPerson;
            return this;
        }

        public StoreBuilder Addresses(params CustomerAddress[] addresses)
        {
            store.Addresses = addresses.ToList();
            return this;
        }

        public StoreBuilder Contacts(params CustomerContact[] contacts)
        {
            store.Contacts = contacts.ToList();
            return this;
        }

        public Store Build()
        {
            return store;
        }
    }
}