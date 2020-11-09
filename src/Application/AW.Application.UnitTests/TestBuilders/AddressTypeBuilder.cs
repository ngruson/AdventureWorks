namespace AW.Application.UnitTests.TestBuilders
{
    public class AddressTypeBuilder
    {
        private Domain.Person.AddressType addressType = new Domain.Person.AddressType();

        public AddressTypeBuilder Name(string name)
        {
            addressType.Name = name;
            return this;
        }

        public Domain.Person.AddressType Build()
        {
            return addressType;
        }

        public AddressTypeBuilder WithTestValues()
        {
            addressType = new Domain.Person.AddressType
            {
                Name = "Main Office"
            };

            return this;
        }
    }
}