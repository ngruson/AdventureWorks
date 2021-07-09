using AW.Services.Customer.Core.Handlers.GetCustomer;

namespace AW.Services.Customer.REST.API.UnitTests.TestBuilders.GetCustomer
{
    public class PersonPhoneBuilder
    {
        private PersonPhoneDto personPhone = new PersonPhoneDto();

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

        public PersonPhoneDto Build()
        {
            return personPhone;
        }

        public PersonPhoneBuilder WithTestValues()
        {
            personPhone = new PersonPhoneDto
            {
                PhoneNumberType = "Cell",
                PhoneNumber = "398-555-0132"
            };

            return this;
        }
    }
}