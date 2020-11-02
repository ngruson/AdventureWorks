using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeleteCustomerContactInfoViewModel : IMapFrom<ContactInfo1>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerContactInfoViewModel CustomerContactInfo { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactInfo1, DeleteCustomerContactInfoViewModel>();
            profile.CreateMap<DeleteCustomerContactInfoViewModel, DeleteCustomerContactInfoRequest>();
        }
    }
}