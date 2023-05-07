using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer
{
    public class PersonEmailAddress : IMapFrom<GetCustomer.PersonEmailAddress>
    {
        public string? EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.PersonEmailAddress, PersonEmailAddress>();
            profile.CreateMap<GetIndividualCustomer.PersonEmailAddress, PersonEmailAddress>();
            profile.CreateMap<GetStoreCustomer.PersonEmailAddress, PersonEmailAddress>();
        }
    }
}