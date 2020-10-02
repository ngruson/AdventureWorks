using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer;

namespace AW.CustomerService.Messages.UpdateCustomer
{
    public class UpdateCustomer : IMapFrom<CustomerDto>
    {
        public string AccountNumber { get; set; }
        public string SalesTerritoryName { get; set; }
        public UpdatePerson Person { get; set; }
        public UpdateStore Store { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerDto, UpdateCustomer>();
        }
    }
}