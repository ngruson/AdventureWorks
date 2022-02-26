using AW.Services.SharedKernel.ValueObjects;

namespace AW.Services.Customer.Core.Entities
{
    public class PersonEmailAddress
    {
        public PersonEmailAddress(EmailAddress emailAddress)
        {
            EmailAddress = emailAddress;
        }
        private PersonEmailAddress()
        {
        }

        private int Id { get; set; }
        public EmailAddress EmailAddress { get; private set; }
    }
}