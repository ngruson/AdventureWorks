using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<GetIndividualCustomer.IndividualCustomer>
    {
        public Person Person { get; set; } = new Person();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetIndividualCustomer.IndividualCustomer, IndividualCustomer>();
        }
    }
}