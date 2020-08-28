using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.SalesOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AW.SalesOrderService.Messages
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
