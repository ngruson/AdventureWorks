using AW.Services.Customer.Application.GetCustomers;

namespace AW.Services.Customer.WCF.UnitTests.TestBuilders.GetCustomers
{
    public class StoreCustomerContactBuilder
    {
        private StoreCustomerContactDto storeCustomerContact = new StoreCustomerContactDto();

        public StoreCustomerContactBuilder ContactType(string contactType)
        {
            storeCustomerContact.ContactType = contactType;
            return this;
        }

        public StoreCustomerContactBuilder ContactPerson(PersonDto contactPerson)
        {
            storeCustomerContact.ContactPerson = contactPerson;
            return this;
        }

        public StoreCustomerContactDto Build()
        {
            return storeCustomerContact;
        }

        public StoreCustomerContactBuilder WithTestValues()
        {
            storeCustomerContact = new StoreCustomerContactDto
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