using MediatR;
using System.Collections.Generic;

namespace AW.Services.Product.Core.Handlers.GetProductCategories
{
    public class GetProductCategoriesQuery : IRequest<List<ProductCategory>>
    {
    }
}