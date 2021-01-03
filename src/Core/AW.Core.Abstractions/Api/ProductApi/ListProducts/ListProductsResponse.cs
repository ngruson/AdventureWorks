using System.Collections.Generic;

namespace AW.Core.Abstractions.Api.ProductApi.ListProducts
{
    public class ListProductsResponse
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int TotalProducts { get; set; }
    }
}