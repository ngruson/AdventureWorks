using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Entities;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class StoreCustomerDto : CustomerDto, IMapFrom<StoreCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Store;
        public string? Name { get; set; }
        public string? SalesPerson { get; set; }
        public List<StoreCustomerContactDto> Contacts { get; set; } = new List<StoreCustomerContactDto>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomer, StoreCustomerDto>();
        }
    }
}