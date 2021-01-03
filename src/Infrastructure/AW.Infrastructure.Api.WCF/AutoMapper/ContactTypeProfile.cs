using AutoMapper;
using AW.Core.Abstractions.Api.ContactTypeApi.ListContactTypes;

namespace AW.Infrastructure.Api.WCF.AutoMapper
{
    public class ContactTypeProfile : Profile
    {
        public ContactTypeProfile()
        {
            //Mappings for ListContactTypes
            CreateMap<ContactTypeService.ListContactTypesResponse, ListContactTypesResponse>();

        }
    }
}