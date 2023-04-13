using MediatR;

namespace AW.Services.Product.Core.Handlers.GetProducts
{
    public class GetProductsQuery : IRequest<GetProductsDto>
    {
        public GetProductsQuery()
        {
        }
        public GetProductsQuery(string category, string subcategory, string orderBy)
        {
            Category = category;
            Subcategory = subcategory;
            OrderBy = orderBy;
        }
        
        public string? Category { get; set; }
        public string? Subcategory { get; set; }
        public string? OrderBy { get; set; }
    }
}
