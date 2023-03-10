using Ardalis.SmartEnum;

namespace AW.Services.Product.Core.Entities
{
    public sealed class Class : SmartEnum<Class, string>
    {
        public static readonly Class High = new(nameof(High), "H");
        public static readonly Class Medium = new(nameof(Medium), "M");
        public static readonly Class Low = new(nameof(Low), "L");

        private Class(string name, string value) : base(name, value) { }
    }
}
