using AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetTerritories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AW.UI.Web.Internal.Controllers
{
    public class SalesTerritoryController : Controller
    {
        private readonly IMediator mediator;

        public SalesTerritoryController(IMediator mediator) =>
            this.mediator = mediator;

        public async Task<IActionResult> Index()
        {
            return View(
                await mediator.Send(new GetTerritoriesQuery())
            );
        }
    }
}