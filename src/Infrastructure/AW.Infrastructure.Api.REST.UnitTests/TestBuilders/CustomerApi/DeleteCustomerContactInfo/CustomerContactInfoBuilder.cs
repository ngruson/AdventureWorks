using AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;

namespace AW.Infrastructure.Api.REST.UnitTests.TestBuilders.CustomerApi.DeleteCustomerContactInfo
{
    public class CustomerContactInfoBuilder
    {
        private CustomerContactInfo customerContactInfo = new CustomerContactInfo();

        public CustomerContactInfoBuilder Channel(Channel channel)
        {
            customerContactInfo.Channel = channel;
            return this;
        }

        public CustomerContactInfoBuilder ContactInfoType(string contactInfoType)
        {
            customerContactInfo.ContactInfoType = contactInfoType;
            return this;
        }

        public CustomerContactInfoBuilder Value(string value)
        {
            customerContactInfo.Value = value;
            return this;
        }

        public CustomerContactInfo Build()
        {
            return customerContactInfo;
        }

        public CustomerContactInfoBuilder WithTestValues()
        {
            customerContactInfo = new CustomerContactInfo
            {
                Channel = Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo.Channel.Phone,
                ContactInfoType = "Cell",
                Value = "245-555-0173"
            };

            return this;
        }
    }
}