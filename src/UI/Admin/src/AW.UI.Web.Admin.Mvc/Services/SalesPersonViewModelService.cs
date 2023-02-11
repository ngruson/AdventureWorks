using AW.UI.Web.Admin.Mvc.ViewModels.SalesPerson;
using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using AW.UI.Web.SharedKernel.SalesPerson.Handlers.GetSalesPersons;
using MediatR;

namespace AW.UI.Web.Admin.Mvc.Services
{
    public class SalesPersonViewModelService : ISalesPersonViewModelService
    {
        private readonly ILogger<SalesPersonViewModelService> logger;
        private readonly IMediator mediator;

        public SalesPersonViewModelService(
            ILogger<SalesPersonViewModelService> logger,
            IMediator mediator
        ) =>
            (this.logger, this.mediator) = (logger, mediator);

        public async Task<SalesPersonIndexViewModel> GetSalesPersons(string? territory = null)
        {
            logger.LogInformation("GetSalesPersons called");

            var salesPersons = await mediator.Send(new GetSalesPersonsQuery(territory));

            var vm = new SalesPersonIndexViewModel
            {
                SalesPersons = salesPersons,
                Territories = await mediator.Send(new GetTerritoriesQuery())
            };

            return vm;
        }
    }
}