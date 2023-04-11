using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProductModel
{
    public class GetProductModelQuery : IRequest<ProductModel>
    {
        public GetProductModelQuery(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
