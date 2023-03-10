using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Product
{
    public class ProductViewModel : IMapFrom<SharedKernel.Product.Handlers.GetProducts.Product>
    {
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }
        public decimal ListPrice { get; set; }
        public string? ProductLine { get; set; }
        public string? Class { get; set; }
        public string? Style { get; set; }
        public byte[]? ThumbnailPhoto { get; set; }
    }
}
