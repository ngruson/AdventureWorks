using AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using AW.UI.Web.Internal.ViewModels;

namespace AW.UI.Web.Internal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, CustomerViewModel>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Store != null ? src.Store.Name : src.Person.FullName))
                .ForMember(m => m.CustomerType, opt => opt.MapFrom(src => src.Store != null ? "Store" :
                    src.Person != null ? "Individual" : null));
        }
    }
}