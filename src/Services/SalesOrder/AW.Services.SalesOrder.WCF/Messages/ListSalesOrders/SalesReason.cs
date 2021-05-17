using AutoMapper;
using AW.Common.AutoMapper;
using AW.Services.SalesOrder.Application.GetSalesOrders;

namespace AW.Services.SalesOrder.WCF.Messages.ListSalesOrders
{
    public class SalesReason : IMapFrom<SalesReasonDto>
    {
        public string Name { get; set; }
        public string ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesReasonDto, SalesReason>();
        }
    }
}