using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using AddCustomerContactInfo = AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;
using DeleteCustomerContactInfo = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerContactInfoViewModel : IMapFrom<AddCustomerContactInfo.CustomerContactInfo>
    {
        public ContactInfoChannelTypeViewModel Channel { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerContactInfoViewModel, AddCustomerContactInfo.CustomerContactInfo>();
            profile.CreateMap<GetCustomer.ContactInfo, CustomerContactInfoViewModel>();

            profile.CreateMap<CustomerContactInfoViewModel, DeleteCustomerContactInfo.CustomerContactInfo>();
        }
    }
}