using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.CreateSalesOrder
{
    public class AddressDto : IMapFrom<Entities.Address>
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryRegionCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, Entities.Address>()
                .ForMember(m => m.Id, opt => opt.Ignore());
            profile.CreateMap<Models.Address, AddressDto>();
        }
    }
}