using AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomers;

namespace AW.UI.Web.Internal.UnitTests.TestBuilders.GetCustomers
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
                    LastName = "Gee"
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