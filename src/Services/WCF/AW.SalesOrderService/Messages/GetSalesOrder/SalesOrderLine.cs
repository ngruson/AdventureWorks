﻿using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.SalesOrder.GetSalesOrder;

namespace AW.SalesOrderService.Messages.GetSalesOrder
{
    public class SalesOrderLine : IMapFrom<SalesOrderLineDto>
    {
        public string CarrierTrackingNumber { get; set; }

        public short OrderQty { get; set; }

        public string ProductName { get; set; }

        public string SpecialOfferDescription { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal UnitPriceDiscount { get; set; }

        public decimal LineTotal { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesOrderLineDto, SalesOrderLine>();
        }
    }
}