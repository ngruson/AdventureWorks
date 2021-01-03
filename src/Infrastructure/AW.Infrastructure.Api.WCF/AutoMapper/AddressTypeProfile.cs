using AutoMapper;
using AW.Core.Abstractions.Api.AddressTypeApi.ListAddressTypes;

namespace AW.Infrastructure.Api.WCF.AutoMapper
{
    public class AddressTypeProfile : Profile
    {
        public AddressTypeProfile()
        {
            //Mappings for ListAddressTypes
            CreateMap<AddressTypeService.ListAddressTypesResponse, ListAddressTypesResponse>();

        }
    }
}