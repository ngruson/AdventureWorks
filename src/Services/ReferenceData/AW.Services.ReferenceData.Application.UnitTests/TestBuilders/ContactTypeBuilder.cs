namespace AW.Services.ReferenceData.Application.UnitTests.TestBuilders
{
    public class ContactTypeBuilder
    {
        private Domain.ContactType contactType = new Domain.ContactType();

        public ContactTypeBuilder Name(string name)
        {
            contactType.Name = name;
            return this;
        }

        public Domain.ContactType Build()
        {
            return contactType;
        }

        public ContactTypeBuilder WithTestValues()
        {
            contactType = new Domain.ContactType
            {
                Id = 1,
                Name = "Owner"
            };

            return this;
        }
    }
}