using System.Collections.Generic;

namespace AW.UI.Web.Store.ApiClients.ProductApi.Models
{
    public class ProductCategory
    {
        public string Name { get; set; }
        public List<ProductSubcategory> Subcategories { get; set; }
    }
}