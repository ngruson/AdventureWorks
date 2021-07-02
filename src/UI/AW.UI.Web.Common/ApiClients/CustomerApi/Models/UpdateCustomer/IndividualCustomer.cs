using AW.Common.AutoMapper;
using AW.Common.Interfaces;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class IndividualCustomer : Customer, IMapFrom<GetCustomer.IndividualCustomer>
    {
        public Person Person { get; set; } = new Person();
    }
}