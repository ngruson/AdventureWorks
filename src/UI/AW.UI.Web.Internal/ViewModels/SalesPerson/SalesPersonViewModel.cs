using AutoMapper;
using AW.Core.Application.AutoMapper;
using ListSalesPersons = AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using GetSalesPerson = AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;

namespace AW.UI.Web.Internal.ViewModels.SalesPerson
{
    public class SalesPersonViewModel : IMapFrom<ListSalesPersons.SalesPerson>
    {
        public string FullName { get; set; }
        public string SalesTerritoryName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ListSalesPersons.SalesPerson, SalesPersonViewModel>();
            profile.CreateMap<GetSalesPerson.SalesPerson, SalesPersonViewModel>();
        }
    }
}