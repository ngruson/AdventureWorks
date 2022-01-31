using AW.Services.Customer.Core.Handlers.GetAllCustomers;
using AW.Services.IdentityServer.Core.Handlers.CreateLogin;
using AW.SharedKernel.AutoMapper;
using System.Globalization;
using System.Text;

namespace AW.ConsoleTools.AutoMapper
{
    public class MappingProfile : BaseMappingProfile
    {
        public MappingProfile()
        {
            CreateMap<IndividualCustomerDto, CreateLoginCommand>()
                .ForMember(m => m.CustomerNumber, opt => opt.MapFrom(src => src.AccountNumber))
                .ForMember(m => m.Username, opt => opt.MapFrom(src => GetUserName(src.Person)))
                .ForMember(m => m.Email, opt => opt.MapFrom(src => GetEmailAddress(src.Person)))
                .ForMember(m => m.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(m => m.MiddleName, opt => opt.MapFrom(src => src.Person.MiddleName))
                .ForMember(m => m.LastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(m => m.FullName, opt => opt.MapFrom(src => src.Person.FullName));
        }

        private static string? GetUserName(PersonDto person)
        {
            var emailAddress = GetEmailAddress(person);

            if (emailAddress != null)
                return Normalize(emailAddress[..emailAddress.IndexOf("@")]);

            return null;
        }

        private static string Normalize(string value)
        {
            var sb = new StringBuilder();
            var arrayText = value.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sb.Append(letter);
            }
            return sb.ToString();
        }

        private static string? GetEmailAddress(PersonDto person)
        {
            if (person.EmailAddresses.Count > 0)
                return person.EmailAddresses[0].EmailAddress;

            return null;
        }
    }
}