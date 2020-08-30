using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomers;
using System.Collections.Generic;

namespace AW.Services.API.CustomerAPI.Models.ListCustomers
{
    public class ListCustomers : IMapFrom<GetCustomersDto>
    {
        public List<Customer> Customer { get; set; } = new List<Customer>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomersDto, ListCustomers>()
                .ForMember(m => m.Customer, opt => opt.MapFrom(src => src.Customers));
        }
    }
}