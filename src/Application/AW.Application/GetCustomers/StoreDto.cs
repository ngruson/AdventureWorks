using AutoMapper;
using AW.Application.Common.Mappings;
using AW.Domain.Sales;

namespace AW.Application.GetCustomers
{
    public class StoreDto : IMapFrom<Store>
    {
        public string Name { get; set; }
        public string SalesPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, StoreDto>()
                .ForMember(m => m.SalesPerson, opt => opt.MapFrom(src => src.SalesPerson.FullName));
        }
    }
}