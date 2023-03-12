using Ardalis.SmartEnum;

namespace AW.Services.Sales.Core.Entities
{
    public class Style : SmartEnum<Style, string>
    {
        public static readonly Style Womens = new(nameof(Womens), "W");
        public static readonly Style Mens = new(nameof(Mens), "M");
        public static readonly Style Universal = new(nameof(Universal), "U");

        public Style(string name, string value) : base(name, value) { }
    }
}
