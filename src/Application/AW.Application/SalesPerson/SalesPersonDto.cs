using AutoMapper;
using AW.Application.AutoMapper;

namespace AW.Application.SalesPerson
{
    public class SalesPersonDto : IMapFrom<Domain.Sales.SalesPerson>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public string SalesTerritoryName { get; set; }

        public decimal? SalesQuota { get; set; }

        public decimal Bonus { get; set; }

        public decimal CommissionPct { get; set; }

        public decimal SalesYTD { get; set; }

        public decimal SalesLastYear { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Sales.SalesPerson, SalesPersonDto>()
                .ReverseMap();
        }
    }
}