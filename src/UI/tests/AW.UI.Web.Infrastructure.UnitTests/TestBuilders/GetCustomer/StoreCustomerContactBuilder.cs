﻿using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomer;

namespace AW.UI.Web.Infrastructure.UnitTests.TestBuilders.GetCustomer
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

        public StoreCustomerContact Build()
        {
            return storeCustomerContact;
        }
    }
}