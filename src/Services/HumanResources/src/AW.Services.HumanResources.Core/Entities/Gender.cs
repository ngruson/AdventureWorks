using Ardalis.SmartEnum;

namespace AW.Services.HumanResources.Core.Entities
{
    public sealed class Gender : SmartEnum<Gender, string>
    {
        public static readonly Gender Male = new(nameof(Male), "M");
        public static readonly Gender Female = new(nameof(Female), "F");

        private Gender(string name, string value) : base(name, value)
        { }
    }
}