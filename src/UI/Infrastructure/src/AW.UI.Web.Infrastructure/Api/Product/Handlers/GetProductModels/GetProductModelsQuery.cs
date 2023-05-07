using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProductModels
{
    public class GetProductModelsQuery : IRequest<List<ProductModel>>
    {
    }
}
