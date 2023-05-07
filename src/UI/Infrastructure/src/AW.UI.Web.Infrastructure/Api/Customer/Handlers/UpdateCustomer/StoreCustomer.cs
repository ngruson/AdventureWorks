using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.UI.Web.Infrastructure.Api.Customer.Handlers.GetStoreCustomer;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer
{
    public class StoreCustomer : Customer, IMapFrom<GetCustomer.StoreCustomer>
    {
        public string? Name { get; set; }
        public string? SalesPerson { get; set; }
        public List<StoreCustomerContact?>? Contacts { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.StoreCustomer, StoreCustomer>();
            profile.CreateMap<GetStoreCustomer.StoreCustomer, StoreCustomer>();
        }
    }
}