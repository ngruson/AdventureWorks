namespace AW.Services.ReferenceData.Core.UnitTests.TestBuilders
{
    public class AddressTypeBuilder
    {
        private Entities.AddressType addressType = new();

        public AddressTypeBuilder Name(string name)
        {
            addressType.Name = name;
            return this;
        }

        public Entities.AddressType Build()
        {
            return addressType;
        }

        public AddressTypeBuilder WithTestValues()
        {
            addressType = new Entities.AddressType
            {
                Id = 1,
                Name = "Home"
            };

            return this;
        }
    }
}