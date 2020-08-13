using MediatR;
using System.Collections.Generic;

namespace AW.Application.GetProducts
{
    public class GetProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}