using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels
{
    public class CustomerStoreViewModel : IMapFrom<Store>
    {
        public string Name { get; set; }
        public string SalesPerson { get; set; }
        public List<SelectListItem> SalesPersons { get; set; }
        public List<CustomerAddressViewModel> Addresses { get; set; }
        public List<ContactViewModel> Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Store, CustomerStoreViewModel>();
            profile.CreateMap<Store1, CustomerStoreViewModel>();
        }
    }
}