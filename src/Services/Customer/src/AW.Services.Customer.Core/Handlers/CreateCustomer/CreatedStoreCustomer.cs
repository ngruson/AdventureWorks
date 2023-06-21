using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class CreatedStoreCustomer : CreatedCustomer, IMapFrom<Entities.StoreCustomer>
    {
        public string? Name { get; set; }
        public string? SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.StoreCustomer, CreatedStoreCustomer>();
        }
    }
}
