using AutoMapper;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPerson;
using AW.Services.SalesPerson.Core.Handlers.GetSalesPersons;
using AW.Services.SalesPerson.WCF.Messages.GetSalesPerson;
using AW.Services.SalesPerson.WCF.Messages.ListSalesPersons;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AW.Services.SalesPerson.WCF
{
    public class SalesPersonService : ISalesPersonService
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public SalesPersonService(IMediator mediator, IMapper mapper) => (this.mediator, this.mapper) = (mediator, mapper);

        public async Task<ListSalesPersonsResponse> ListSalesPersons(ListSalesPersonsRequest request)
        {
            var salesPersons = await mediator.Send(new GetSalesPersonsQuery
            {
                Territory = request.Territory
            });

            var response = new ListSalesPersonsResponse
            {
                SalesPersons = mapper.Map<List<Core.Models.SalesPerson>>(salesPersons)
            };

            return response;
        }

        public async Task<GetSalesPersonResponse> GetSalesPerson(GetSalesPersonRequest request)
        {
            var salesPerson = await mediator.Send(new GetSalesPersonQuery
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName
            });

            return new GetSalesPersonResponse
            {
                SalesPerson = mapper.Map<Core.Models.SalesPerson>(salesPerson)
            };
        }
    }
}