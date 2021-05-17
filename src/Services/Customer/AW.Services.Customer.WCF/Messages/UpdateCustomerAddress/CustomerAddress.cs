﻿using AW.Services.Customer.Application.UpdateCustomerAddress;
using System.Xml.Serialization;
using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.Customer.WCF.Messages.UpdateCustomerAddress
{
    [XmlType(Namespace = "http://services.aw.com/CustomerService/1.0/UpdateCustomerAddress")]
    public class CustomerAddress : IMapFrom<CustomerAddressDto>
    {
        public string AddressType { get; set; }
        public Address Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerAddress, CustomerAddressDto>();

        }
    }
}