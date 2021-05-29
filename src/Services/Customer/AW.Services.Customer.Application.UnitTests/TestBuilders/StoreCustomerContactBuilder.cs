using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Application.UnitTests.TestBuilders
{
    public class StoreCustomerContactBuilder
    {
        private StoreCustomerContact storeCustomerContact = new StoreCustomerContact();

        public StoreCustomerContactBuilder ContactType(string contactType)
        {
            storeCustomerContact.ContactType = contactType;
            return this;
        }

        public StoreCustomerContactBuilder ContactPerson(Person contactPerson)
        {
            storeCustomerContact.ContactPerson = contactPerson;
            return this;
        }

        public StoreCustomerContact Build()
        {
            return storeCustomerContact;
        }

        public StoreCustomerContactBuilder WithTestValues()
        {
            storeCustomerContact = new StoreCustomerContact
            {
                ContactType = "Owner",
                ContactPerson = new PersonBuilder()
                    .WithTestValues()
                    .Build()
            };

            return this;
        }
    }
}