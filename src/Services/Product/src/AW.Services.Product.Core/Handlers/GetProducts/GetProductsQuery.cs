using MediatR;

namespace AW.Services.Product.Core.Handlers.GetProducts
{
    public class GetProductsQuery : IRequest<GetProductsDto>
    {
        public GetProductsQuery()
        {
        }
        public GetProductsQuery(int pageIndex, int pageSize, string category, string subcategory, string orderBy)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Category = category;
            Subcategory = subcategory;
            OrderBy = orderBy;
        }

        public int PageIndex { get; private init; }
        public int PageSize { get; private init; }
        public string? Category { get; private init; }
        public string? Subcategory { get; private init; }
        public string? OrderBy { get; private init; }
    }
}
