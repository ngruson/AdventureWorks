using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Sales.Core.Entities;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class SalesReasonDto : IMapFrom<SalesReason>
    {
        public string? Name { get; set; }
        public string? ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesReason, SalesReasonDto>()
                .ReverseMap();
        }
    }
}