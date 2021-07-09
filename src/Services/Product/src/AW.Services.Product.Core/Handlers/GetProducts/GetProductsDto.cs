using System.Collections.Generic;

namespace AW.Services.Product.Core.Handlers.GetProducts
{
    public class GetProductsDto
    {
        public List<Product> Products { get; set; }
        public int TotalProducts { get; set; }
    }
}