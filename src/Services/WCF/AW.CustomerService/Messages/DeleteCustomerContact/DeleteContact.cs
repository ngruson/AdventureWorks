using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.DeleteCustomerContact;

namespace AW.CustomerService.Messages.DeleteCustomerContact
{
    public class DeleteContact : IMapFrom<ContactDto>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteContact, ContactDto>();
        }
    }
}