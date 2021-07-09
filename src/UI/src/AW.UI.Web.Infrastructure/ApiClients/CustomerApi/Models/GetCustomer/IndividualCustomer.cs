using AW.SharedKernel.Extensions;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.GetCustomer
{
    public class IndividualCustomer : Customer
    {
        public override string CustomerName => Person.FullName();
        public Person Person { get; set; }
    }
}