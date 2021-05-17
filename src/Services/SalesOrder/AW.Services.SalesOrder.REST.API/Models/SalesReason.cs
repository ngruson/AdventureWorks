using AutoMapper;
using AW.Common.AutoMapper;

namespace AW.Services.SalesOrder.REST.API.Models
{
    public class SalesReason : IMapFrom<Application.GetSalesOrders.SalesReasonDto>
    {
        public string Name { get; set; }
        public string ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Application.GetSalesOrders.SalesReasonDto, SalesReason>();
            profile.CreateMap<Application.GetSalesOrder.SalesReasonDto, SalesReason>();
        }
    }
}