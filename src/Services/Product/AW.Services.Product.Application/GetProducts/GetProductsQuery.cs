using MediatR;
using System.Collections.Generic;

namespace AW.Services.Product.Application.GetProducts
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