using Ardalis.SmartEnum;

namespace AW.Services.HumanResources.Core.Entities
{
    public sealed class MaritalStatus : SmartEnum<MaritalStatus, string>
    {
        public static readonly MaritalStatus Married = new(nameof(Married), "M");
        public static readonly MaritalStatus Single = new(nameof(Single), "S");

        private MaritalStatus(string name, string value) : base(name, value)
        { }
    }
}