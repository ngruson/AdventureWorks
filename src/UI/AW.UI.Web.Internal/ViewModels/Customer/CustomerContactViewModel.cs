using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerContactViewModel : IMapFrom<CustomerContact>
    {
        [Display(Name = "Contact type")]
        [Required]
        public string ContactType { get; set; }
        public ContactViewModel Contact { get; set; } = new ContactViewModel();
        public string EmailAddress { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContact, CustomerContactViewModel>();
            profile.CreateMap<CustomerContact1, CustomerContactViewModel>();
            profile.CreateMap<CustomerContactViewModel, CustomerContact2>();
            profile.CreateMap<CustomerContactViewModel, CustomerContact3>();
        }
    }
}