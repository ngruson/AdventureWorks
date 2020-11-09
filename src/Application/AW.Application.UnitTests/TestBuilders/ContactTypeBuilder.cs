namespace AW.Application.UnitTests.TestBuilders
{
    public class ContactTypeBuilder
    {
        private Domain.Person.ContactType contactType = new Domain.Person.ContactType();

        public ContactTypeBuilder Name(string name)
        {
            contactType.Name = name;
            return this;
        }

        public Domain.Person.ContactType Build()
        {
            return contactType;
        }

        public ContactTypeBuilder WithTestValues()
        {
            contactType = new Domain.Person.ContactType
            {
                Name = "Owner"
            };

            return this;
        }
    }
}