using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductCategories
{
    public class GetProductCategoriesQuery : IRequest<List<ProductCategory>>
    {
    }
}