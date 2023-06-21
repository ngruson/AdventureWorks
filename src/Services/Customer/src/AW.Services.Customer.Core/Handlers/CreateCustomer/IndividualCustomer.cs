using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<Entities.IndividualCustomer>
    {
        public IndividualCustomer() { }
        public IndividualCustomer(Person person)
        {
            Person = person;
        }
        public Person Person { get; set; } = new Person();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IndividualCustomer, Entities.IndividualCustomer>()
                .ForMember(_ => _.ObjectId, opt => opt.Ignore())
                .ForMember(m => m.SalesOrders, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
