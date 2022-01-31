using AW.SharedKernel.AutoMapper;
using System.Collections.Generic;

namespace AW.Services.Customer.Core.Handlers.GetAllCustomers
{
    public class PersonDto : IMapFrom<Entities.Person>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Suffix { get; set; }
        public List<PersonEmailAddressDto> EmailAddresses { get; set; }
    }
}