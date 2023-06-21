namespace AW.Services.Customer.Core.Entities
{
    public class StoreCustomerContact
    {
        private StoreCustomerContact() { }
        
        public StoreCustomerContact(string contactType, Person contactPerson)
        {
            ContactType = contactType;
            ContactPerson = contactPerson;
        }

        public StoreCustomerContact(Guid objectId, string contactType, Person contactPerson)
        {
            ObjectId = objectId;
            ContactType = contactType;
            ContactPerson = contactPerson;
        }

        public int Id { get; set; }
        public Guid ObjectId { get; set; }
        public string? ContactType { get; private set; }

        public int StoreCustomerId { get; private set; }

        public Person? ContactPerson { get; private set; }
        public int ContactPersonId { get; private set; }
    }
}
