using AutoMapper;
using AW.Services.Sales.Core.Entities;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrdersForCustomer
{
    public class SalesReasonDto : IMapFrom<SalesReason>
    {
        public string? Name { get; set; }
        public string? ReasonType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesReason, SalesReasonDto>();
        }
    }
}