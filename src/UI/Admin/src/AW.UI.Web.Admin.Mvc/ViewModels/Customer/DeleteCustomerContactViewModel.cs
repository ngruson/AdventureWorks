using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetStoreCustomer;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class DeleteCustomerContactViewModel : IMapFrom<StoreCustomerContact>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Contact type")]
        public string ContactType { get; set; }

        public PersonViewModel ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContact, DeleteCustomerContactViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.CustomerName, opt => opt.Ignore())
                .ForMember(m => m.ContactType, opt => opt.MapFrom(src => src.ContactType));
        }
    }
}