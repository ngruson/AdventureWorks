using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer;

namespace AW.CustomerService.Messages.UpdateCustomer
{
    public class UpdateStore : IMapFrom<StoreCustomerDto>
    {
        public string Name { get; set; }
        public UpdateSalesPerson SalesPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerDto, UpdateStore>();
        }
    }
}