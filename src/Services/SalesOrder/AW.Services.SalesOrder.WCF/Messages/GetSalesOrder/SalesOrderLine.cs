using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Application.GetSalesOrder;

namespace AW.Services.SalesOrder.WCF.Messages.GetSalesOrder
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