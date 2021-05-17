using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.Application.DeleteStoreCustomerContact
{
    public class PersonDto : IMapFrom<Domain.Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Person, PersonDto>();
        }
    }
}