using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.UpdateCustomer
{
    public class Person : IMapFrom<GetCustomer.Person>
    {
        public string? Title { get; set; }
        public NameFactory? Name { get; set; }
        public string? Suffix { get; set; }
        public List<PersonEmailAddress>? EmailAddresses { get; set; }
        public List<PersonPhone>? PhoneNumbers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.Person, Person>();
            profile.CreateMap<GetIndividualCustomer.Person, Person>();
            profile.CreateMap<GetStoreCustomer.Person, Person>();
        }
    }
}