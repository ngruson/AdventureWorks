using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AW.Core.Abstractions.Api.ProductApi.GetProduct
{
    public class GetProductResponse
    {
        public Product Product { get; set; } = new Product();
    }
}
