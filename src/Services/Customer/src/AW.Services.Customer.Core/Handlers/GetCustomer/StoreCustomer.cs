using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class StoreCustomer : Customer, IMapFrom<Entities.StoreCustomer>
    {
        public override CustomerType CustomerType { get; set; } = CustomerType.Store;
        
        public string? Name { get; set; }
        public string? SalesPerson { get; set; }
        public List<StoreCustomerContact> Contacts { get; set; } = new List<StoreCustomerContact>();
    }
}
