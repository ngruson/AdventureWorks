using AutoMapper;
using AW.Services.ReferenceData.Application.Common;

namespace AW.Services.ReferenceData.Application.AddressType.GetAddressTypes
{
    public class AddressType : IMapFrom<Domain.AddressType>
    {
        public string Name { get; set; }

        #if NETSTANDARD2_0
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.AddressType, AddressType>();
        }
        #endif
    }
}