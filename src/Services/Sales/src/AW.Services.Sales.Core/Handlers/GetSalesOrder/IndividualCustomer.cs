using AutoMapper;
using AW.SharedKernel.AutoMapper;
using AW.SharedKernel.ValueTypes;

namespace AW.Services.Sales.Core.Handlers.GetSalesOrder
{
    public class IndividualCustomer : Customer, IMapFrom<Entities.IndividualCustomer>
    {
        public string? Title { get; set; }
        public NameFactory? Name { get; private set; }
        public override string? CustomerName => Name?.FullName;
        public string? Suffix { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.IndividualCustomer, IndividualCustomer>()
                .ForMember(_ => _.Title, opt => opt.MapFrom(src => src.Person.Title))
                .ForMember(_ => _.Name, opt => opt.MapFrom(src => src.Person.Name))
                .ForMember(_ => _.Suffix, opt => opt.MapFrom(src => src.Person.Suffix));
        }
    }    
}
