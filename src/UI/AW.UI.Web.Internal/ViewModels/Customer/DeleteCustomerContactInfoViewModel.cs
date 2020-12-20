using AutoMapper;
using AW.Core.Application.AutoMapper;
using GetCustomer = AW.Core.Abstractions.Api.CustomerApi.GetCustomer;
using DeleteCustomerContactInfo = AW.Core.Abstractions.Api.CustomerApi.DeleteCustomerContactInfo;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeleteCustomerContactInfoViewModel : IMapFrom<GetCustomer.ContactInfo>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerContactInfoViewModel CustomerContactInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.ContactInfo, DeleteCustomerContactInfoViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.CustomerName, opt => opt.Ignore())
                .ForMember(m => m.CustomerContactInfo, opt => opt.MapFrom(src => src));
            profile.CreateMap<DeleteCustomerContactInfoViewModel, DeleteCustomerContactInfo.DeleteCustomerContactInfoRequest>();
        }
    }
}