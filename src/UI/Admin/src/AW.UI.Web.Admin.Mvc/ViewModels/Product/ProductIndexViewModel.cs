namespace AW.UI.Web.Admin.Mvc.ViewModels.Product
{
    public class ProductIndexViewModel
    {
        public ProductIndexViewModel(List<ProductViewModel> products)
        {
            Products = products;
        }
        public List<ProductViewModel> Products { get; set; }
    }
}
