using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.Services.Customer.Core.Handlers.GetCustomers;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.Services.Customer.WCF.Messages.ListCustomers
{
    public class ListCustomers : IMapFrom<GetCustomersDto>
    {
        [XmlElement(Namespace = "http://services.aw.com/CustomerService/1.0/ListCustomers")]
        public List<Core.Models.GetCustomers.Customer> Customer { get; set; } = new List<Core.Models.GetCustomers.Customer>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomersDto, ListCustomers>()
                .ForMember(m => m.Customer, opt => opt.MapFrom(src => src.Customers));
        }
    }
}