using System.Collections.Generic;

namespace AW.Services.Product.Application.GetProducts
{
    public class GetProductsDto
    {
        public List<Product> Products { get; set; }
        public int TotalProducts { get; set; }
    }
}