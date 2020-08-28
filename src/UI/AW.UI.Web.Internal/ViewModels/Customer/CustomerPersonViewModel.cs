using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerPersonViewModel : IMapFrom<Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public EmailPromotionViewModel EmailPromotion { get; set; }
        public List<CustomerAddressViewModel> Addresses { get; set; }
        public List<ContactInfoViewModel> ContactInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Person, CustomerPersonViewModel>();
            profile.CreateMap<Person1, CustomerPersonViewModel>();
        }
    }
}