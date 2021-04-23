using AW.UI.Web.Internal.Extensions;

namespace AW.UI.Web.Internal.ApiClients.CustomerApi.Models.GetCustomer
{
    public class IndividualCustomer : Customer
    {
        public override string CustomerName => Person.FullName();
        public Person Person { get; set; }
    }
}