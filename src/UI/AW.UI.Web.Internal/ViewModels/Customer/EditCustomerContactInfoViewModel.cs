using AutoMapper;
using AW.Core.Application.AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using AW.Core.Abstractions.Api.CustomerApi.AddCustomerContactInfo;

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