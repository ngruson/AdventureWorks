using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.UnitTests.TestBuilders
{
    public class EmailAddressBuilder
    {
        private PersonEmailAddress personEmailAddress = new();

        public EmailAddressBuilder EmailAddress(string emailAddress)
        {
            personEmailAddress.EmailAddress = emailAddress;
            return this;
        }

        public PersonEmailAddress Build()
        {
            return personEmailAddress;
        }

        public EmailAddressBuilder WithTestValues()
        {
            personEmailAddress = new PersonEmailAddress
            {
                EmailAddress = "jon24@adventure-works.com"
            };

            return this;
        }
    }
}