using AutoMapper;
using AW.Core.Application.AutoMapper;
using AW.Infrastructure.Api.WCF.SalesPersonService;

namespace AW.UI.Web.Internal.ViewModels.SalesPerson
{
    public class SalesPersonViewModel : IMapFrom<SalesPersonDto>
    {
        public string FullName { get; set; }
        public string SalesTerritoryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesPersonDto, SalesPersonViewModel>();
        }
    }
}