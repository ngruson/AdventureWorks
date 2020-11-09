using AW.Domain.Person;

namespace AW.Application.UnitTests.TestBuilders
{
    public class BusinessEntityContactBuilder
    {
        private BusinessEntityContact businessEntityContact = new BusinessEntityContact();

        public BusinessEntityContactBuilder Person(Person person)
        {
            businessEntityContact.Person = person;
            return this;
        }

        public BusinessEntityContactBuilder ContactType(Domain.Person.ContactType contactType)
        {
            businessEntityContact.ContactType = contactType;
            return this;
        }

        public BusinessEntityContact Build()
        {
            return businessEntityContact;
        }

        public BusinessEntityContactBuilder WithTestValues()
        {
            businessEntityContact = new BusinessEntityContact
            {
                Person = new PersonBuilder().WithTestValues().Build(),
                ContactType = new ContactTypeBuilder().WithTestValues().Build()
            };

            return this;
        }
    }
}