using AutoMapper;
using AW.Application.AutoMapper;
using AW.Application.Customer.GetCustomers;

namespace AW.Services.API.CustomerAPI.Models.ListCustomers
{
    public class ListCustomersResponse : IMapFrom<GetCustomersDto>
    {
        public int TotalCustomers { get; set; }
        public ListCustomers Customers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetCustomersDto, ListCustomersResponse>()
                .ForMember(m => m.Customers, opt => opt.MapFrom(src => src));
        }
    }
}