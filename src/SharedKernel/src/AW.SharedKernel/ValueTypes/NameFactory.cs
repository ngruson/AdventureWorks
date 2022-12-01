namespace AW.SharedKernel.ValueTypes
{
    public record NameFactory(string FirstName, string? MiddleName, string LastName) : INameFactory
    {
        public string FirstName { get; set; } = FirstName;
        public string? MiddleName { get; set; } = MiddleName;
        public string LastName { get; set; } = LastName;
        public string FullName => INameFactory.GetFullName(this);
    }
}