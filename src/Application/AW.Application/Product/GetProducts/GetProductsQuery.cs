using MediatR;
using System.Collections.Generic;

namespace AW.Application.Product.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}