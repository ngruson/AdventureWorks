using AW.Services.Customer.Domain;

namespace AW.Services.Customer.Application.UnitTests.TestBuilders
{
    public class PersonPhoneBuilder
    {
        private PersonPhone personPhone = new PersonPhone();

        public PersonPhoneBuilder PhoneNumberType(string phoneNumberType)
        {
            personPhone.PhoneNumberType = phoneNumberType;
            return this;
        }

        public PersonPhoneBuilder PhoneNumber(string phoneNumber)
        {
            personPhone.PhoneNumber = phoneNumber;
            return this;
        }

        public PersonPhone Build()
        {
            return personPhone;
        }

        public PersonPhoneBuilder WithTestValues()
        {
            personPhone = new PersonPhone
            {
                PhoneNumberType = "Cell",
                PhoneNumber = "398-555-0132"
            };

            return this;
        }
    }
}