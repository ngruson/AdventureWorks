using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProductCategories
{
    public class GetProductCategoriesQuery : IRequest<List<ProductCategory>>
    {
    }
}