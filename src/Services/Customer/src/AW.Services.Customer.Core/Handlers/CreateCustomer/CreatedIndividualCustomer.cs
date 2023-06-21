using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.CreateCustomer
{
    public class CreatedIndividualCustomer : CreatedCustomer, IMapFrom<Entities.IndividualCustomer>
    {
        public Person Person { get; set; } = new Person();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.IndividualCustomer, CreatedIndividualCustomer>();
        }
    }
}
