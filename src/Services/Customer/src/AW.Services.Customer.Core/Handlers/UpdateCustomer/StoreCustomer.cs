using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class StoreCustomer : Customer, IMapFrom<Entities.StoreCustomer>
    {
        public override CustomerType CustomerType { get; set; } = CustomerType.Store;
        public string? Name { get; set; }
        public string? SalesPerson { get; set; }
        public List<StoreCustomerContact>? Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.StoreCustomer, StoreCustomer>()
                .ReverseMap();
        }
    }
}
