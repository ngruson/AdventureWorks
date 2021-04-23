using AW.UI.Web.Internal.Common;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<GetCustomer.IndividualCustomer>
    {
        public override CustomerType CustomerType => CustomerType.Individual;
        public Person Person { get; set; } = new Person();
    }
}