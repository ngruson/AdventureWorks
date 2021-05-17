using AutoMapper;
using AW.Common.AutoMapper;
using System.ComponentModel.DataAnnotations;
using m = AW.UI.Web.Common.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeleteCustomerContactViewModel : IMapFrom<m.GetCustomer.StoreCustomerContact>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Contact type")]
        public string ContactType { get; set; }

        public PersonViewModel ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.GetCustomer.StoreCustomerContact, DeleteCustomerContactViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.CustomerName, opt => opt.Ignore())
                .ForMember(m => m.ContactType, opt => opt.MapFrom(src => src.ContactType));
        }
    }
}