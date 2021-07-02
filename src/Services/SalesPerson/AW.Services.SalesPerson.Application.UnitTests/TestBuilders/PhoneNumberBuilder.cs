using AW.Services.SalesPerson.Domain;

namespace AW.Services.SalesPerson.Application.UnitTests.TestBuilders
{
    public class PhoneNumberBuilder
    {
        private SalesPersonPhone phone = new();

        public PhoneNumberBuilder PhoneNumberType(string phoneNumberType)
        {
            phone.PhoneNumberType = phoneNumberType;
            return this;
        }

        public PhoneNumberBuilder PhoneNumber(string phoneNumber)
        {
            phone.PhoneNumber = phoneNumber;
            return this;
        }

        public SalesPersonPhone Build()
        {
            return phone;
        }

        public PhoneNumberBuilder WithTestValues()
        {
            phone = new SalesPersonPhone
            {
                PhoneNumberType = "Cell",
                PhoneNumber = "238-555-0197"
            };

            return this;
        }
    }
}