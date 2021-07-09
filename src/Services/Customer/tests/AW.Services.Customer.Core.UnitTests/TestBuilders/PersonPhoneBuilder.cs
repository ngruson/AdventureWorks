using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.UnitTests.TestBuilders
{
    public class PersonPhoneBuilder
    {
        private PersonPhone personPhone = new();

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