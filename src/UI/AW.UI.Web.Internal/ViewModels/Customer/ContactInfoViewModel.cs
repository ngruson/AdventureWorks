using AutoMapper;
using AW.Core.Application.AutoMapper;
using ListCustomers = AW.Core.Abstractions.Api.CustomerApi.ListCustomers;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using UpdateCustomer = AW.Core.Abstractions.Api.CustomerApi.UpdateCustomer;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class ContactInfoViewModel : IMapFrom<ListCustomers.ContactInfo>
    {
        public ContactInfoChannelTypeViewModel Channel { get; set; }
        public string ContactInfoType { get; set; }
        public string Value { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListCustomers.ContactInfo, ContactInfoViewModel>();
            profile.CreateMap<GetCustomer.ContactInfo, ContactInfoViewModel>();

            profile.CreateMap<ContactInfoViewModel, UpdateCustomer.ContactInfo>();
        }
    }
}