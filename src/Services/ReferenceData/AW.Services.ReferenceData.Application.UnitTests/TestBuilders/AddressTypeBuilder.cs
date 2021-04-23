namespace AW.Services.ReferenceData.Application.UnitTests.TestBuilders
{
    public class AddressTypeBuilder
    {
        private Domain.AddressType addressType = new Domain.AddressType();

        public AddressTypeBuilder Name(string name)
        {
            addressType.Name = name;
            return this;
        }

        public Domain.AddressType Build()
        {
            return addressType;
        }

        public AddressTypeBuilder WithTestValues()
        {
            addressType = new Domain.AddressType
            {
                Id = 1,
                Name = "Home"
            };

            return this;
        }
    }
}