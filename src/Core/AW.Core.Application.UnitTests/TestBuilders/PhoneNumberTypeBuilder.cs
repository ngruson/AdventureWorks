using AW.Core.Domain.Person;

namespace AW.Core.Application.UnitTests.TestBuilders
{
    public class PhoneNumberTypeBuilder
    {
        private PhoneNumberType phoneNumberType = new PhoneNumberType();

        public PhoneNumberTypeBuilder Name(string name)
        {
            phoneNumberType.Name = name;
            return this;
        }

        public PhoneNumberType Build()
        {
            return phoneNumberType;
        }

        public PhoneNumberTypeBuilder WithTestValues()
        {
            phoneNumberType = new PhoneNumberType
            {
                Name = "Cell"
            };

            return this;
        }
    }
}