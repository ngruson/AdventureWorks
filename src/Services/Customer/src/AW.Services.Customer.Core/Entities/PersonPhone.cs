namespace AW.Services.Customer.Core.Entities
{
    public class PersonPhone
    {
        public PersonPhone(string phoneNumberType, string phoneNumber)
        {
            PhoneNumberType = phoneNumberType;
            PhoneNumber = phoneNumber;
        }

        private int Id { get; set; }
        public string PhoneNumberType { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}