using AutoMapper;
using AW.Common.AutoMapper;
using System.ComponentModel.DataAnnotations;
using m = AW.UI.Web.Common.ApiClients.CustomerApi.Models;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeleteCustomerAddressViewModel : IMapFrom<m.GetCustomer.CustomerAddress>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Address type")]
        public string AddressType { get; set; }

        public AddressViewModel Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<m.GetCustomer.CustomerAddress, DeleteCustomerAddressViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.CustomerName, opt => opt.Ignore());
        }
    }
}