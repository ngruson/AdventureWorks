using AW.Services.Customer.Core.Handlers.GetCustomers;

namespace AW.Services.Customer.REST.API.UnitTests.TestBuilders.GetCustomers
{
    public class EmailAddressBuilder
    {
        private PersonEmailAddressDto personEmailAddress = new PersonEmailAddressDto();

        public EmailAddressBuilder EmailAddress(string emailAddress)
        {
            personEmailAddress.EmailAddress = emailAddress;
            return this;
        }

        public PersonEmailAddressDto Build()
        {
            return personEmailAddress;
        }

        public EmailAddressBuilder WithTestValues()
        {
            personEmailAddress = new PersonEmailAddressDto
            {
                EmailAddress = "jon24@adventure-works.com"
            };

            return this;
        }
    }
}