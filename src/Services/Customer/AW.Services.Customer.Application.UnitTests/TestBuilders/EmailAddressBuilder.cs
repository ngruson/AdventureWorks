using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Application.UnitTests.TestBuilders
{
    public class EmailAddressBuilder
    {
        private PersonEmailAddress personEmailAddress = new PersonEmailAddress();

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