using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.Infrastructure.ApiClients.CustomerApi.Models.UpdateCustomer
{
    public class PersonEmailAddress : IMapFrom<GetCustomer.PersonEmailAddress>
    {
        public string EmailAddress { get; set; }
    }
}