using AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class SalesPersonDto : IMapFrom<SalesPerson>
    {
        public NameFactory? Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesPerson, SalesPersonDto>()
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}