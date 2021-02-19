using MediatR;
using System.Collections.Generic;

namespace AW.Services.Product.Application.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}