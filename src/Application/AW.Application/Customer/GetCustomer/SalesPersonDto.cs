using AutoMapper;
using AW.Application.AutoMapper;

namespace AW.Application.Customer.GetCustomer
{
    public class SalesPersonDto : IMapFrom<Domain.Sales.SalesPerson>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Sales.SalesPerson, SalesPersonDto>();
        }
    }
}