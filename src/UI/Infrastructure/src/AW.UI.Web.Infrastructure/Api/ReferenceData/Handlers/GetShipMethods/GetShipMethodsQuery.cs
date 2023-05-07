using MediatR;

namespace AW.UI.Web.Infrastructure.Api.ReferenceData.Handlers.GetShipMethods
{
    public class GetShipMethodsQuery : IRequest<List<ShipMethod>>
    {
    }
}