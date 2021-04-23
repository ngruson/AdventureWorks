using AutoMapper;
using AW.Services.ReferenceData.Application.Common;

namespace AW.Services.ReferenceData.Application.ContactType.GetContactTypes
{
    public class ContactType : IMapFrom<Domain.ContactType>
    {
        public string Name { get; set; }

        #if NETSTANDARD2_0
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.ContactType, ContactType>();
        }
        #endif
    }
}