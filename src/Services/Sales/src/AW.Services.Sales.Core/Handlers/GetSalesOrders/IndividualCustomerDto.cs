using AutoMapper;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrders
{
    public class IndividualCustomerDto : CustomerDto, IMapFrom<Entities.IndividualCustomer>
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.IndividualCustomer, IndividualCustomerDto>()
                .ForMember(_ => _.Title, opt => opt.MapFrom(src => src.Person.Title))
                .ForMember(_ => _.FirstName, opt => opt.MapFrom(src => src.Person.FirstName))
                .ForMember(_ => _.MiddleName, opt => opt.MapFrom(src => src.Person.MiddleName))
                .ForMember(_ => _.LastName, opt => opt.MapFrom(src => src.Person.LastName))
                .ForMember(_ => _.Suffix, opt => opt.MapFrom(src => src.Person.Suffix));
        }
    }    
}