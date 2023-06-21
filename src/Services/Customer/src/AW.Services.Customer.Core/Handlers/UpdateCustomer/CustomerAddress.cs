using AutoMapper;
using AutoMapper.EquivalencyExpression;
using AW.SharedKernel.AutoMapper;

namespace AW.Services.Customer.Core.Handlers.UpdateCustomer
{
    public class CustomerAddress : IMapFrom<Entities.CustomerAddress>
    {
        public Guid ObjectId { get; set; }
        public string? AddressType { get; set; }
        public Address? Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Entities.CustomerAddress, CustomerAddress>()
                .ReverseMap()
                .EqualityComparison((src, dest) => src.ObjectId == dest.ObjectId);
        }
    }
}
