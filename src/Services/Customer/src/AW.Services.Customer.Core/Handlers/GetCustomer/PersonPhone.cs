using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.GetCustomer
{
    public class PersonPhone : IMapFrom<Entities.PersonPhone>
    {
        public Guid ObjectId { get; set; }
        public string? PhoneNumberType { get; set; }
        public string? PhoneNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.PersonPhone, PersonPhone>();
        }
    }
}
