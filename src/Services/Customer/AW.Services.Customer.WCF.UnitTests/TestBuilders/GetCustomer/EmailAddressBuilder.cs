using AW.Services.Customer.Application.GetCustomer;

namespace AW.Services.Customer.WCF.UnitTests.TestBuilders.GetCustomer
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