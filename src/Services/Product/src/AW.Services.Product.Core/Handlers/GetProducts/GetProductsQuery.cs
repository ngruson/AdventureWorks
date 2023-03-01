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

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Category { get; set; }
        public string? Subcategory { get; set; }
        public string? OrderBy { get; set; }
    }
}
