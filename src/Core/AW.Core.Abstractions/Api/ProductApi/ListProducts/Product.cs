namespace AW.Core.Abstractions.Api.ProductApi.ListProducts
{
    public class Product
    {
        public string Name { get; set; }
        public string ProductNumber { get; set; }
        public string Color { get; set; }
        public decimal ListPrice { get; set; }
        public string Size { get; set; }
        public string SizeUnitMeasureCode { get; set; }
        public decimal Weight { get; set; }
        public string WeightUnitMeasureCode { get; set; }
        public string ProductLine { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }
        public string ProductSubcategoryName { get; set; }
        public string ProductCategoryName { get; set; }
        public byte[] LargePhoto { get; set; }
    }
}