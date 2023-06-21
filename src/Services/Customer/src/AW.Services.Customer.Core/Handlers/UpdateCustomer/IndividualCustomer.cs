using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.Interfaces;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<Entities.IndividualCustomer>
    {
        public override CustomerType CustomerType { get; set; } = CustomerType.Store;        
        public Person Person { get; set; } = new Person();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.IndividualCustomer, IndividualCustomer>()
                .ReverseMap();
        }
    }
}
