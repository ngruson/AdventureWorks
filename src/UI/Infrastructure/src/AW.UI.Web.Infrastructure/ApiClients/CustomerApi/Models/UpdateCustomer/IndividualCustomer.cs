using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<GetCustomer.IndividualCustomer>
    {
        public Person Person { get; set; } = new Person();
    }
}