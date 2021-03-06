﻿using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.SalesOrder.GetSalesOrder;

namespace AW.SalesOrderService.Messages.GetSalesOrder
{
    public class Address : IMapFrom<AddressDto>
    {
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string StateProvinceName { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddressDto, Address>();
        }
    }
}