using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.SalesOrder.GetSalesOrder;

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