using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class SalesPerson : IMapFrom<Entities.SalesPerson>
    {
        public NameFactory? Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.SalesPerson, SalesPerson>()
                .ReverseMap();
        }
    }
}
