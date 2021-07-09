namespace AW.Services.ReferenceData.Core.UnitTests.TestBuilders
{
    public class ContactTypeBuilder
    {
        private Entities.ContactType contactType = new();

        public ContactTypeBuilder Name(string name)
        {
            contactType.Name = name;
            return this;
        }

        public Entities.ContactType Build()
        {
            return contactType;
        }

        public ContactTypeBuilder WithTestValues()
        {
            contactType = new Entities.ContactType
            {
                Id = 1,
                Name = "Owner"
            };

            return this;
        }
    }
}