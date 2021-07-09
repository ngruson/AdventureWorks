using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes
{
    public class AddressType : IMapFrom<Core.Entities.AddressType>
    {
        public string Name { get; set; }

        #if NETSTANDARD2_0
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.AddressType, AddressType>();
        }
        #endif
    }
}