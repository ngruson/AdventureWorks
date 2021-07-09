using MediatR;

namespace AW.Services.Product.Core.Handlers.GetProducts
{
    public class GetProductsQuery : IRequest<GetProductsDto>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string OrderBy { get; set; }
    }
}