using System.Collections.Generic;

namespace AW.UI.Web.Common.ApiClients.ProductApi.Models
{
    public class ProductCategory
    {
        public string Name { get; set; }
        public List<ProductSubcategory> Subcategories { get; set; }
    }
}