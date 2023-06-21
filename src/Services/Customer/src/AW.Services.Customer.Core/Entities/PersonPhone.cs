namespace AW.Services.Customer.Core.Entities
{
    public class PersonPhone
    {
        public PersonPhone(string phoneNumberType, string phoneNumber)
        {
            PhoneNumberType = phoneNumberType;
            PhoneNumber = phoneNumber;
        }
        public PersonPhone(Guid objectId, string phoneNumberType, string phoneNumber)
        {
            ObjectId = objectId;
            PhoneNumberType = phoneNumberType;
            PhoneNumber = phoneNumber;
        }

        public int Id { get; private set; }
        public Guid ObjectId { get; private set; }
        public string PhoneNumberType { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
