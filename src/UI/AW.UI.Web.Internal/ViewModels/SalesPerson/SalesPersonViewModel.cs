using AutoMapper;
using AW.Application.AutoMapper;
using AW.UI.Web.Internal.SalesPersonService;

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