using AW.Common.AutoMapper;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<GetCustomer.IndividualCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Individual;
        public Person Person { get; set; } = new Person();
    }
}