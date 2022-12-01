namespace AW.SharedKernel.ValueTypes
{
    public interface INameFactory
    {
        string FirstName { get; set; }
        string? MiddleName { get; set; }
        string LastName { get; set; }

        string FullName => GetFullName(this);
        
        protected static string GetFullName(INameFactory nameFactory)
        {
            string fullName = nameFactory.FirstName;
            if (!string.IsNullOrEmpty(nameFactory.MiddleName))
                fullName += $" {nameFactory.MiddleName}";
            if (!string.IsNullOrEmpty(nameFactory.LastName))
                fullName += $" {nameFactory.LastName}";

            return fullName;
        }
    }
}