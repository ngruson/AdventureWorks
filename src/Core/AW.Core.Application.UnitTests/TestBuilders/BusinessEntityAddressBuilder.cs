using AW.Core.Domain.Person;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class BusinessEntityAddressBuilder
    {
        private BusinessEntityAddress businessEntityAddress = new BusinessEntityAddress();

        public BusinessEntityAddressBuilder Address(Address address)
        {
            businessEntityAddress.Address = address;
            return this;
        }

        public BusinessEntityAddressBuilder AddressType(Domain.Person.AddressType addressType)
        {
            businessEntityAddress.AddressType = addressType;
            return this;
        }

        public BusinessEntityAddress Build()
        {
            return businessEntityAddress;
        }

        public BusinessEntityAddressBuilder WithTestValues()
        {
            businessEntityAddress = new BusinessEntityAddress
            {
                Address = new AddressBuilder().WithTestValues().Build(),
                AddressType = new AddressTypeBuilder().WithTestValues().Build()
            };

            return this;
        }
    }
}