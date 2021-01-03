using AutoMapper;
using ListSalesPersons = AW.Core.Abstractions.Api.SalesPersonApi.ListSalesPersons;
using GetSalesPerson = AW.Core.Abstractions.Api.SalesPersonApi.GetSalesPerson;

namespace AW.Infrastructure.Api.WCF.AutoMapper
{
    public class SalesPersonProfile : Profile
    {
        public SalesPersonProfile()
        {
            //Mappings for ListSalesOrders
            CreateMap<ListSalesPersons.ListSalesPersonsRequest, SalesPersonService.ListSalesPersonsRequest1>()
                .ForMember(m => m.request, opt => opt.MapFrom(src => src));
            CreateMap<ListSalesPersons.ListSalesPersonsRequest, SalesPersonService.ListSalesPersonsRequest>();
            CreateMap<SalesPersonService.ListSalesPersonsResponse, ListSalesPersons.ListSalesPersonsResponse>()
                .ForMember(m => m.SalesPersons, opt => opt.MapFrom(src => src.ListSalesPersonsResult));
            CreateMap<SalesPersonService.SalesPersonDto, ListSalesPersons.SalesPerson>();

            //Mappings for GetProduct
            CreateMap<GetSalesPerson.GetSalesPersonRequest, SalesPersonService.GetSalesPersonRequest>();
            CreateMap<SalesPersonService.GetSalesPersonResponseGetSalesPersonResult, GetSalesPerson.GetSalesPersonResponse>();
            CreateMap<SalesPersonService.SalesPersonDto1, GetSalesPerson.SalesPerson>();
        }
    }
}