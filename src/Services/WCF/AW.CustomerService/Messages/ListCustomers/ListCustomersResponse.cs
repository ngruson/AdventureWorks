﻿using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.GetCustomers;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.ListCustomers
{
    [XmlType(AnonymousType = true, Namespace = "http://services.aw.com/CustomerService/1.0")]
    [XmlRoot(Namespace = "http://services.aw.com/CustomerService/1.0", IsNullable = false)]
    public class ListCustomersResponse : IMapFrom<GetCustomersDto>
    {
        public int TotalCustomers { get; set; }
        public ListCustomers Customers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomersDto, ListCustomersResponse>()
                .ForMember(m => m.Customers, opt => opt.MapFrom(src => src));
        }
    }
}