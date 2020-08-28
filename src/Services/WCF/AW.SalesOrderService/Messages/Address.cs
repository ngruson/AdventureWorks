using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.SalesOrder;
using System;

namespace AW.SalesOrderService.Messages
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