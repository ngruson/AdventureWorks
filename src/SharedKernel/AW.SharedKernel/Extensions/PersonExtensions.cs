using AW.SharedKernel.Interfaces;

namespace AW.SharedKernel.Extensions
{
    public static class PersonExtensions
    {
        public static string FullName(this IPerson person)
        {
            string fullName = "";

            if (!string.IsNullOrEmpty(person.FirstName))
                fullName = person.FirstName;

            if (!string.IsNullOrEmpty(person.MiddleName))
                fullName += $" {person.MiddleName}";

            if (!string.IsNullOrEmpty(person.LastName))
                fullName += $" {person.LastName}";

            return fullName;
        }
    }
}