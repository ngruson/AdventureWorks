using AW.Services.SalesPerson.Domain;

namespace AW.Services.SalesPerson.Application.UnitTests.TestBuilders
{
    public class EmailAddressBuilder
    {
        private SalesPersonEmailAddress emailAddress = new();

        public EmailAddressBuilder EmailAddress(string email)
        {
            emailAddress.EmailAddress = email;
            return this;
        }

        public SalesPersonEmailAddress Build()
        {
            return emailAddress;
        }

        public EmailAddressBuilder WithTestValues()
        {
            emailAddress = new SalesPersonEmailAddress
            {
                EmailAddress = "stephen0@adventure-works.com"
            };

            return this;
        }
    }
}