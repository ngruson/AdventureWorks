using AW.Core.Application.SalesTerritory.GetSalesTerritories;
using AW.SalesTerritoryService.Messages;
using MediatR;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace AW.SalesTerritoryService
{
    [ServiceBehavior(Namespace = "http://services.aw.com/SalesTerritoryService/1.0")]
    public class SalesTerritoryService : ISalesTerritoryService
    {
        private readonly IMediator mediator;

        public SalesTerritoryService(IMediator mediator) =>
            this.mediator = mediator;

        public async Task<ListTerritoriesResponse> ListTerritories()
        {
            var territories = await mediator.Send(new GetSalesTerritoriesQuery());

            var response = new ListTerritoriesResponse
            {
                Territories = territories.ToList()
            };

            return response;
        }
    }
}