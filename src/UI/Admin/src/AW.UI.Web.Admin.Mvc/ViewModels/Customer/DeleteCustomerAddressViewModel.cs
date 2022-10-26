using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.SharedKernel.Customer.Handlers.GetIndividualCustomer;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Admin.Mvc.ViewModels.Customer
{
    public class DeleteCustomerAddressViewModel : IMapFrom<CustomerAddress>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Address type")]
        public string AddressType { get; set; }

        public AddressViewModel Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, DeleteCustomerAddressViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.CustomerName, opt => opt.Ignore());
        }
    }
}