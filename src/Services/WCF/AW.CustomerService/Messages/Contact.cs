using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomer;

namespace AW.CustomerService.Messages
{
    public class Contact : IMapFrom<ContactDto>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactDto, Contact>();
        }
    }
}