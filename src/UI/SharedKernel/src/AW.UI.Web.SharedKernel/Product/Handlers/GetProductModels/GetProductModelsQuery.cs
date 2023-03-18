using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProductModels
{
    public class GetProductModelsQuery : IRequest<List<ProductModel>>
    {
    }
}
