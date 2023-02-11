using AW.Services.Customer.Core.Handlers.GetAllCustomers;
using AW.Services.IdentityServer.Core.Handlers.CreateLogin;
using AW.SharedKernel.AutoMapper;
using System.Reflection;

namespace AW.ConsoleTools.AutoMapper
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            CreateMap<IndividualCustomerDto, CreateLoginCommand>()
                .ForMember(m => m.CustomerNumber, opt => opt.MapFrom(src => src.AccountNumber))
                .ForMember(m => m.Username, opt => opt.MapFrom(src => GetUserName(src.Person!)))
                .ForMember(m => m.Email, opt => opt.MapFrom(src => GetEmailAddress(src.Person!)))
                .ForMember(m => m.Name, opt => opt.MapFrom(src => src.Person!.Name));
        }

        private static string? GetUserName(PersonDto person)
        {
            var emailAddress = GetEmailAddress(person);

            if (emailAddress != null)
                return emailAddress[..emailAddress.IndexOf("@")].Normalize();

            return null;
        }

        private static string? GetEmailAddress(PersonDto person)
        {
            if (person.EmailAddresses.Count > 0)
                return person.EmailAddresses[0].EmailAddress;

            return null;
        }
    }
}