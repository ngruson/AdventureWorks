using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.DeleteStoreCustomerContact
{
    public class PersonDto : IMapFrom<Entities.Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.Person, PersonDto>();
        }
    }
}