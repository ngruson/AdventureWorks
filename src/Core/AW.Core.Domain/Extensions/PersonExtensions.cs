namespace AW.Core.Domain.Person
{
    public static class PersonExtensions
    {
        public static string FullName(this Person person)
        {
            string fullName = person.FirstName;

            if (!string.IsNullOrEmpty(person.MiddleName))
                fullName += " " + person.MiddleName;

            if (!string.IsNullOrEmpty(person.LastName))
                fullName += " " + person.LastName;

            return fullName;
        }
    }
}