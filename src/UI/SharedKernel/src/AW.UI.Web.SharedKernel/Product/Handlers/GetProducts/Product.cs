namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProducts
{
    public class Product
    {
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }        
        public decimal ListPrice { get; set; }
        public string? Size { get; set; }
        public string? SizeUnitMeasureCode { get; set; }
        public decimal Weight { get; set; }
        public string? WeightUnitMeasureCode { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }
        public string? ProductSubcategoryName { get; set; }
        public string? ProductCategoryName { get; set; }
        public List<ProductProductPhoto>? ProductProductPhotos { get; set; }

        public ProductPhoto? GetPrimaryPhoto()
        {
            var photo = ProductProductPhotos?.SingleOrDefault(_ => _.Primary);
            return photo?.ProductPhoto;
        }
    }
}
