using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class CustomerStoreViewModel : IMapFrom<Store>
    {
        public string Name { get; set; }
        
        public SalesPersonViewModel SalesPerson { get; set; }
        public List<CustomerAddressViewModel> Addresses { get; set; }
        public List<ContactViewModel> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, CustomerStoreViewModel>();
            profile.CreateMap<Store1, CustomerStoreViewModel>();
            profile.CreateMap<CustomerStoreViewModel, UpdateStore>();
        }
    }
}