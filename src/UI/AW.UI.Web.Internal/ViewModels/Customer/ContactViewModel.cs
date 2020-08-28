using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class ContactViewModel : IMapFrom<Contact>
    {
        public string ContactTypeName { get; set; }
        public string ContactName { get; set; }
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactViewModel>();
            profile.CreateMap<Contact1, ContactViewModel>();
        }
    }
}