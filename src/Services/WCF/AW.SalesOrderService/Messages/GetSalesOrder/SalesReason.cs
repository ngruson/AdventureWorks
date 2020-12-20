using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.SalesOrder.GetSalesOrder;

namespace AW.SalesOrderService.Messages.GetSalesOrder
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