using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.ReferenceData.Core.Handlers.ContactType.GetContactTypes
{
    public class ContactType : IMapFrom<Core.Entities.ContactType>
    {
        public string? Name { get; private init; }

        #if NETSTANDARD2_0
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.ContactType, ContactType>();
        }
        #endif
    }
}