using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Core.Application.Customer.UpdateCustomer;

namespace AW.CustomerService.Messages.UpdateCustomer
{
    public class UpdateStore : IMapFrom<StoreCustomerDto>
    {
        public string Name { get; set; }
        public UpdateSalesPerson SalesPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateStore, StoreCustomerDto>()
                .ForMember(m => m.Addresses, opt => opt.Ignore())
                .ForMember(m => m.Contacts, opt => opt.Ignore());
        }
    }
}