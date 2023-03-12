namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProduct
{
    public class Product
    {
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }
        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public string? Color { get; set; }
        public short SafetyStockLevel { get; set; }
        public short ReorderPoint { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string? Size { get; set; }
        public string? SizeUnitMeasureCode { get; set; }
        public decimal? Weight { get; set; }
        public string? WeightUnitMeasureCode { get; set; }
        public int DaysToManufacture { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }
        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public DateTime? DiscontinuedDate { get; set; }
        public string? ProductSubcategoryName { get; set; }
        public string? ProductCategoryName { get; set; }
        public byte[]? ThumbnailPhoto { get; set; }
        public byte[]? LargePhoto { get; set; }
    }
}
