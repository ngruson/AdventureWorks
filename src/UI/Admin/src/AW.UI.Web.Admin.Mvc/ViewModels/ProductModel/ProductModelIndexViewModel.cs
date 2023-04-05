namespace AW.UI.Web.Admin.Mvc.ViewModels.ProductModel
{
    public class ProductModelIndexViewModel
    {
        public ProductModelIndexViewModel(List<ProductModelViewModel> productModels)
        {
            ProductModels = productModels;
        }

        public List<ProductModelViewModel> ProductModels { get; set; }
    }
}
