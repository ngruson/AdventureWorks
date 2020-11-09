using AW.Domain.Person;

namespace AW.Application.UnitTests.TestBuilders
{
    public class EmailAddressBuilder
    {
        private EmailAddress emailAddress = new EmailAddress();

        public EmailAddressBuilder EmailAddress1(string emailAddress1)
        {
            emailAddress.EmailAddress1 = emailAddress1;
            return this;
        }

        public EmailAddress Build()
        {
            return emailAddress;
        }

        public EmailAddressBuilder WithTestValues()
        {
            emailAddress = new EmailAddress
            {
                EmailAddress1 = "demo@adventure-works.com"
            };

            return this;
        }
    }
}