using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomer;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomer
{
    public class PersonBuilder
    {
        private Person person = new Person();

        public PersonBuilder Title(string title)
        {
            person.Title = title;
            return this;
        }

        public PersonBuilder Name(PersonName name)
        {
            person.Name = name;
            return this;
        }

        public PersonBuilder Suffix(string suffix)
        {
            person.Suffix = suffix;
            return this;
        }

        public PersonBuilder WithTestValues()
        {
            person = new Person
            {
                Name = new PersonName
                {
                    FirstName = "Orlando",
                    MiddleName = "N.",
                    LastName = "Gee",
                    FullName = "Orlando N. Gee"
                }
            };

            return this;
        }

        public Person Build()
        {
            return person;
        }
    }
}