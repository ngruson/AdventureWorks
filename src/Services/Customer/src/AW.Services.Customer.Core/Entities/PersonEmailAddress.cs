using AW.Services.SharedKernel.ValueTypes;

namespace AW.Services.Customer.Core.Entities
{
    public class PersonEmailAddress
    {
        public PersonEmailAddress(EmailAddress emailAddress)
        {
            EmailAddress = emailAddress;
        }
        public PersonEmailAddress(Guid objectId)
        {
            ObjectId = objectId;
        }
        private PersonEmailAddress()
        {
        }

        public int Id { get; set; }
        public Guid ObjectId { get; set; }
        public EmailAddress? EmailAddress { get; private set; }
        public int PersonId { get; private set; }
    }
}
