using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.UI.Web.SharedKernel.Customer.Handlers.UpdateCustomer
{
    public class PersonPhone : IMapFrom<GetCustomer.PersonPhone>
    {
        public string? PhoneNumberType { get; set; }
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomer.PersonPhone, PersonPhone>();
            profile.CreateMap<GetIndividualCustomer.PersonPhone, PersonPhone>();
            profile.CreateMap<GetStoreCustomer.PersonPhone, PersonPhone>();
        }
    }
}