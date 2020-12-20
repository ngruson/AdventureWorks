using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Domain.Sales;

namespace AW.Core.Application.SalesOrder.GetSalesOrders
{
    public class SalesReasonDto : IMapFrom<SalesReason>
    {
        public string Name { get; set; }
        public string ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesReason, SalesReasonDto>();
        }
    }
}