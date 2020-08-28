using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomers;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace AW.CustomerService.Messages.ListCustomers
{
    public class ListCustomers : IMapFrom<GetCustomersDto>
    {
        [XmlElement(Namespace = "http://services.aw.com/CustomerService/1.0/ListCustomers")]
        public List<Customer> Customer { get; set; } = new List<Customer>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomersDto, ListCustomers>()
                .ForMember(m => m.Customer, opt => opt.MapFrom(src => src.Customers));
        }
    }
}