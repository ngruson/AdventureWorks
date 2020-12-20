using System;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class AddressBuilder
    {
        private Domain.Person.Address address = new Domain.Person.Address();

        public AddressBuilder Id(int id)
        {
            address.Id = id;
            return this;
        }

        public AddressBuilder AddressLine1(string addressLine1)
        {
            address.AddressLine1 = addressLine1;
            return this;
        }

        public AddressBuilder AddressLine2(string addressLine2)
        {
            address.AddressLine2 = addressLine2;
            return this;
        }

        public AddressBuilder City(string city)
        {
            address.City = city;
            return this;
        }

        public AddressBuilder StateProvince(Domain.Person.StateProvince stateProvince)
        {
            address.StateProvince = stateProvince;
            return this;
        }

        public AddressBuilder PostalCode(string postalCode)
        {
            address.PostalCode = postalCode;
            return this;
        }

        public Domain.Person.Address Build()
        {
            return address;
        }

        public AddressBuilder WithTestValues()
        {
            address = new Domain.Person.Address
            {
                Id = new Random().Next(),
                AddressLine1 = "2251 Elliot Avenue",
                City = "Seattle",
                StateProvince = new StateProvinceBuilder().WithTestValues().Build(),
                PostalCode = "98104"
            };

            return this;
        }
    }
}