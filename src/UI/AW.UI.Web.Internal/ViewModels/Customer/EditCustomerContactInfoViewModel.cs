using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class EditCustomerContactInfoViewModel : IMapFrom<AddCustomerContactInfoRequest>
    {
        public bool IsNewContactInfo{ get; set; }
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public CustomerContactInfoViewModel CustomerContactInfo { get; set; }
        public IEnumerable<SelectListItem> ChannelTypes { get; set; }
        public IEnumerable<SelectListItem> ContactInfoTypes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<EditCustomerContactInfoViewModel, AddCustomerContactInfoRequest>();
        }
    }
}