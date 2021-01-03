using AutoMapper;
using ListProducts = AW.Core.Abstractions.Api.ProductApi.ListProducts;
using GetProduct = AW.Core.Abstractions.Api.ProductApi.GetProduct;

namespace AW.Infrastructure.Api.WCF.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //Mappings for ListProducts
            CreateMap<ListProducts.ListProductsRequest, ProductService.ListProductsRequest>();
            CreateMap<ProductService.ListProductsResponseListProductsResult, ListProducts.ListProductsResponse>();
            CreateMap<ProductService.ProductDto, ListProducts.Product>();

            //Mappings for GetProduct
            CreateMap<GetProduct.GetProductRequest, ProductService.GetProductRequest>();
            CreateMap<ProductService.GetProductResponseGetProductResult, GetProduct.GetProductResponse>();
            CreateMap<ProductService.ProductDto1, GetProduct.Product>();
        }
    }
}