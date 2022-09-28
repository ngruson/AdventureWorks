using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.AddressType.GetAddressTypes
{
    public class AddressType : IMapFrom<Entities.AddressType>
    {
        public string? Name { get; private init; }
    }
}