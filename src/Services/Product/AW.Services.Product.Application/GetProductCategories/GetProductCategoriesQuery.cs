using MediatR;
using System.Collections.Generic;

namespace AW.Services.Product.Application.GetProductCategories
{
    public class GetProductCategoriesQuery : IRequest<List<ProductCategory>>
    {
    }
}