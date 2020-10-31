using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class ContactViewModel : IMapFrom<Contact>
    {
        public string Title { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Contact, ContactViewModel>();
            profile.CreateMap<Contact1, ContactViewModel>();
            profile.CreateMap<ContactViewModel, Contact2>();
            profile.CreateMap<ContactViewModel, Contact3>();
        }
    }
}