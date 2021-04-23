using AutoMapper;
using AW.Services.Customer.Application.Common;
using AW.Services.Customer.Application.GetCustomers;
using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.ListCustomers
{
    public class StoreCustomerContact : IMapFrom<StoreCustomerContactDto>
    {
        public string ContactType { get; set; }
        public Person ContactPerson { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<StoreCustomerContactDto, StoreCustomerContact>();
        }
    }
}