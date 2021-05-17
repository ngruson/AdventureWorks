using AW.Common.AutoMapper;

namespace AW.UI.Web.Common.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class PersonEmailAddress : IMapFrom<GetCustomer.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }
    }
}