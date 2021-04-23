using AW.UI.Web.Internal.ApiClients.ReferenceDataApi.Models.GetAddressTypes;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetAddressTypes
{
    public class AddressTypeBuilder
    {
        private AddressType addressType = new AddressType();

        public AddressTypeBuilder Name(string name)
        {
            addressType.Name = name;
            return this;
        }

        public AddressType Build()
        {
            return addressType;
        }
    }
}