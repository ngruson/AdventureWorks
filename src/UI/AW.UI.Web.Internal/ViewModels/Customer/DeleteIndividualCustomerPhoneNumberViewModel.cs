﻿using AutoMapper;
using AW.Common.AutoMapper;
using AW.UI.Web.Common.ApiClients.CustomerApi.Models.GetCustomer;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeleteIndividualCustomerPhoneNumberViewModel : IMapFrom<PersonPhone>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumberType { get; set; }
        public string PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PersonPhone, DeleteIndividualCustomerPhoneNumberViewModel>()
                .ForMember(m => m.AccountNumber, opt => opt.Ignore())
                .ForMember(m => m.CustomerName, opt => opt.Ignore());
        }
    }
}