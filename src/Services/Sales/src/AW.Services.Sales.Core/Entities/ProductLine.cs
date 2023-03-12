using Ardalis.SmartEnum;

namespace AW.Services.Sales.Core.Entities
{
    public class ProductLine : SmartEnum<ProductLine, string>
    {
        public static readonly ProductLine Road = new(nameof(Road), "R");
        public static readonly ProductLine Mountain = new(nameof(Mountain), "M");
        public static readonly ProductLine Touring = new(nameof(Touring), "T");
        public static readonly ProductLine Standard = new(nameof(Standard), "S");
        
        public ProductLine(string name, string value) : base(name, value) { }
    }
}
