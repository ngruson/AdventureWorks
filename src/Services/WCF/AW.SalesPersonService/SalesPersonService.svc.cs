using AW.Core.Application.SalesPerson.GetSalesPerson;
using AW.Core.Application.SalesPerson.GetSalesPersons;
using AW.SalesPersonService.Messages.GetSalesPerson;
using AW.SalesPersonService.Messages.ListSalesPersons;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.SalesPersonService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/SalesPersonService/1.0")]
    public class SalesPersonService : ISalesPersonService
    {
        private readonly IMediator mediator;

        public SalesPersonService(IMediator mediator) =>
            this.mediator = mediator;        

        public async Task<ListSalesPersonsResponse> ListSalesPersons(ListSalesPersonsRequest request)
        {
            var salesPersons = await mediator.Send(new GetSalesPersonsQuery
            {
                SalesTerritoryName = request.Territory
            });

            var response = new ListSalesPersonsResponse
            {
                SalesPersons = salesPersons.ToList()
            };

            return response;
        }

        public async Task<GetSalesPersonResponse> GetSalesPerson(GetSalesPersonRequest request)
        {
            var salesPerson = await mediator.Send(new GetSalesPersonQuery
            {
                FullName = request.FullName
            });

            return new GetSalesPersonResponse
            {
                SalesPerson = salesPerson
            };
        }
    }
}