using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class SalesReason : IMapFrom<Core.Handlers.GetSalesOrders.SalesReasonDto>
    {
        public string Name { get; set; }
        public string ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Handlers.GetSalesOrders.SalesReasonDto, SalesReason>();
            profile.CreateMap<Core.Handlers.GetSalesOrder.SalesReasonDto, SalesReason>();
        }
    }
}