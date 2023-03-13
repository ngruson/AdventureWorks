using AW.Services.SharedKernel.Interfaces;

namespace AW.Services.Product.Core.Entities
{
    public class Product : IAggregateRoot
    {
        public Product()
        {
        }

        public Product(string productNumber)
        {
            ProductNumber = productNumber;
        }
        public Product(string productNumber, string color)
        {
            ProductNumber = productNumber;
            Color = color;
        }

        public int Id { get; set; }
        
        public string? Name { get; private set; }
       
        public string? ProductNumber { get; private set; }

        public bool MakeFlag { get; private set; }

        public bool FinishedGoodsFlag { get; private set; }
        
        public string? Color { get; private set; }

        public short SafetyStockLevel { get; private set; }

        public short ReorderPoint { get; private set; }

        public decimal StandardCost { get; private set; }

        public decimal ListPrice { get; private set; }

        public string? Size { get; private set; }

        public string? SizeUnitMeasureCode { get; private set; }

        public string? WeightUnitMeasureCode { get; private set; }
        public decimal? Weight { get; private set; }

        public int DaysToManufacture { get; private set; }

        public ProductLine? ProductLine { get; private set; }

        public Class? Class { get; private set; }

        public Style? Style { get; private set; }

        public int? ProductSubcategoryId { get; private set; }

        public int? ProductModelId { get; private set; }

        public DateTime SellStartDate { get; private set; }

        public DateTime? SellEndDate { get; private set; }

        public DateTime? DiscontinuedDate { get; private set; }

        public ProductModel? ProductModel { get; private set; }

        public virtual ProductSubcategory? ProductSubcategory { get; private set; }

        public virtual UnitMeasure? SizeUnitMeasure { get; private set; }

        public virtual UnitMeasure? WeightUnitMeasure { get; private set; }

        public List<ProductProductPhoto> ProductProductPhotos { get; internal set; } = new();

        public void AddProductPhoto(ProductProductPhoto photo)
        {
            ProductProductPhotos.Add(photo);
        }

        public void SetPrimaryPhoto(ProductProductPhoto photo)
        {
            foreach (var item in ProductProductPhotos)
            {
                item.Primary = photo == item;
            }
        }
    }
}
