using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.UpdateSalesOrder
{
    public class StoreCustomer : Customer, IMapFrom<Entities.StoreCustomer>
    {
        public string? Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.StoreCustomer, StoreCustomer>()
                .ReverseMap();
        }
    }    
}