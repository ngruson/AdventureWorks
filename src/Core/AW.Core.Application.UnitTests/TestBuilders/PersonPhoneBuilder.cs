using AW.Core.Domain.Person;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class PersonPhoneBuilder
    {
        private PersonPhone personPhone = new PersonPhone();

        public PersonPhoneBuilder PhoneNumber(string phoneNumber)
        {
            personPhone.PhoneNumber = phoneNumber;
            return this;
        }

        public PersonPhoneBuilder PhoneNumberType(PhoneNumberType phoneNumberType)
        {
            personPhone.PhoneNumberType = phoneNumberType;
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
                PhoneNumber = "245-555-0173",
                PhoneNumberType = new PhoneNumberTypeBuilder().WithTestValues().Build()
            };

            return this;
        }
    }
}