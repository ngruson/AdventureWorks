using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.Api.Customer.Handlers.UpdateCustomer;

public class StoreCustomer : Customer, IMapFrom<GetCustomer.StoreCustomer>
{
    public StoreCustomer() { }

    public StoreCustomer(Guid objectId)
    {
        ObjectId = objectId;
    }

    public string? Name { get; set; }
    public string? SalesPerson { get; set; }
    public List<StoreCustomerContact?>? Contacts { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GetCustomer.StoreCustomer, StoreCustomer>();
        profile.CreateMap<GetStoreCustomer.StoreCustomer, StoreCustomer>();
    }
}
