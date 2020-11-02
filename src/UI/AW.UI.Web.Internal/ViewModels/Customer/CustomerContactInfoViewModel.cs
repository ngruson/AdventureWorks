using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerContactInfoViewModel : IMapFrom<CustomerContactInfo>
    {
        public ContactInfoChannelTypeViewModel Channel { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContactInfoViewModel, CustomerContactInfo>();
            profile.CreateMap<ContactInfo1, CustomerContactInfoViewModel>();
            profile.CreateMap<CustomerContactInfoViewModel, CustomerContactInfo1>();
        }
    }
}