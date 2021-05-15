using System.Collections.Generic;

namespace AW.UI.Web.Store.ApiClients.ProductApi.Models
{
    public class GetProductsResult
    {
        public List<Product> Products { get; set; }
        public int TotalProducts { get; set; }
    }
}