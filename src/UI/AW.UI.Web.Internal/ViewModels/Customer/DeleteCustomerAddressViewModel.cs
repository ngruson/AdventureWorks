﻿using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.CustomerService;
using System.ComponentModel.DataAnnotations;

namespace AW.UI.Web.Internal.ViewModels.Customer
{
    public class DeleteCustomerAddressViewModel : IMapFrom<CustomerAddress1>
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Address type")]
        public string AddressType { get; set; }

        [Display(Name = "Address line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address line 2")]
        public string AddressLine2 { get; set; }

        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State/province")]
        public string StateProvinceName { get; set; }

        [Display(Name = "Country")]
        public string CountryRegionName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress1, DeleteCustomerAddressViewModel>()
                .ForMember(m => m.AddressLine1, opt => opt.MapFrom(src => src.Address.AddressLine1))
                .ForMember(m => m.AddressLine2, opt => opt.MapFrom(src => src.Address.AddressLine2))
                .ForMember(m => m.PostalCode, opt => opt.MapFrom(src => src.Address.PostalCode))
                .ForMember(m => m.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(m => m.StateProvinceName, opt => opt.MapFrom(src => src.Address.StateProvince.Name))
                .ForMember(m => m.CountryRegionName, opt => opt.MapFrom(src => src.Address.StateProvince.CountryRegion.Name));
        }
    }
}