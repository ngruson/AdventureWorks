namespace AW.Services.SharedKernel.ValueTypes
{
    public record NameFactory(string FirstName, string? MiddleName = null, string? LastName = null)
    {
        public string FullName
        {
            get
            {
                string fullName = FirstName;
                if (!string.IsNullOrEmpty(MiddleName))
                    fullName += $" {MiddleName}";
                if (!string.IsNullOrEmpty(LastName))
                    fullName += $" {LastName}";

                return fullName;
            }
        }
    }
}