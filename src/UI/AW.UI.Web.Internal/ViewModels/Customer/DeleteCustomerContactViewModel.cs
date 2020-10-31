using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeleteCustomerContactViewModel : IMapFrom<CustomerContact1>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Contact type")]
        public string ContactType { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string FullName { get; set; }

        [Display(Name = "Suffix")]
        public string Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContact1, DeleteCustomerContactViewModel>()
                .ForMember(m => m.Title, opt => opt.MapFrom(src => src.Contact.Title))
                .ForMember(m => m.FirstName, opt => opt.MapFrom(src => src.Contact.FirstName))
                .ForMember(m => m.MiddleName, opt => opt.MapFrom(src => src.Contact.MiddleName))
                .ForMember(m => m.LastName, opt => opt.MapFrom(src => src.Contact.LastName))
                .ForMember(m => m.Suffix, opt => opt.MapFrom(src => src.Contact.Suffix));

            profile.CreateMap<DeleteCustomerContactViewModel, DeleteCustomerContactRequest>()
                .ForMember(m => m.Contact, opt => opt.MapFrom(src => src));

            profile.CreateMap<DeleteCustomerContactViewModel, DeleteContact>();
        }
    }
}