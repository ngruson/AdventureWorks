using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using DeleteCustomerContact = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContact;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeleteCustomerContactViewModel : IMapFrom<GetCustomer.CustomerContact>
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
            profile.CreateMap<GetCustomer.CustomerContact, DeleteCustomerContactViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.CustomerName, opt => opt.Ignore())
                .ForMember(m => m.ContactType, opt => opt.MapFrom(src => src.ContactType))
                .ForMember(m => m.Title, opt => opt.MapFrom(src => src.Contact.Title))
                .ForMember(m => m.FirstName, opt => opt.MapFrom(src => src.Contact.FirstName))
                .ForMember(m => m.MiddleName, opt => opt.MapFrom(src => src.Contact.MiddleName))
                .ForMember(m => m.LastName, opt => opt.MapFrom(src => src.Contact.LastName))
                .ForMember(m => m.FullName, opt => opt.MapFrom(src => src.Contact.FullName))
                .ForMember(m => m.Suffix, opt => opt.MapFrom(src => src.Contact.Suffix));

            profile.CreateMap<DeleteCustomerContactViewModel, DeleteCustomerContact.DeleteCustomerContactRequest>()
                .ForMember(m => m.Contact, opt => opt.MapFrom(src => src));

            profile.CreateMap<DeleteCustomerContactViewModel, DeleteCustomerContact.Contact>();
        }
    }
}