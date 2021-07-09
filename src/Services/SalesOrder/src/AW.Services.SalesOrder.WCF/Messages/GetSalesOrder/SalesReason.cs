using AutoMapper;
using AW.Services.SalesOrder.Core.Handlers.GetSalesOrder;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.SalesOrder.WCF.Messages.GetSalesOrder
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