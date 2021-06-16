using AW.Services.Customer.Application.GetCustomer;

namespace AW.Services.Customer.REST.API.UnitTests.TestBuilders.GetCustomer
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