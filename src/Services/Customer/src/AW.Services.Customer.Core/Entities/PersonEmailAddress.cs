namespace AW.Services.Customer.Core.Entities
{
    public class PersonEmailAddress
    {
        public PersonEmailAddress(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        private int Id { get; set; }
        public string EmailAddress { get; private set; }
    }
}