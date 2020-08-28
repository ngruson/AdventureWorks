using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.SalesOrder;

namespace AW.SalesOrderService.Messages
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