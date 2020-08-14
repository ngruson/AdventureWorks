using AutoMapper;
using AW.UI.Web.External.ProductService;
using AW.UI.Web.External.ViewModels;

namespace AW.UI.Web.External
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDto, ProductViewModel>();
        }
    }
}