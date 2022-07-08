using MediatR;

namespace AW.UI.Web.SharedKernel.Product.Handlers.GetProducts
{
    public class GetProductsQuery : IRequest<GetProductsResult>
    {
        public GetProductsQuery(int pageIndex, int pageSize, string? category, string? subcategory, string? orderBy = null)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Category = category;
            Subcategory = subcategory;
            OrderBy = orderBy;
        }

        public int PageIndex { get; init; }
        public int PageSize { get; init; }
        public string? Category { get; init; }
        public string? Subcategory { get; init; }
        public string? OrderBy { get; init; }
    }
}