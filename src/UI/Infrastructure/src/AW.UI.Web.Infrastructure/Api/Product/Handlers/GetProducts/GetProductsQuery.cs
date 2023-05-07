using MediatR;

namespace AW.UI.Web.Infrastructure.Api.Product.Handlers.GetProducts
{
    public class GetProductsQuery : IRequest<GetProductsResult>
    {
        public GetProductsQuery(string? category, string? subcategory, string? orderBy = null)
        {
            Category = category;
            Subcategory = subcategory;
            OrderBy = orderBy;
        }

        public string? Category { get; init; }
        public string? Subcategory { get; init; }
        public string? OrderBy { get; init; }
    }
}
