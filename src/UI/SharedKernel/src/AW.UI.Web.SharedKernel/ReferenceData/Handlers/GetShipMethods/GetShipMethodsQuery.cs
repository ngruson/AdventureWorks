using MediatR;

namespace AW.UI.Web.SharedKernel.ReferenceData.Handlers.GetShipMethods
{
    public class GetShipMethodsQuery : IRequest<List<ShipMethod>>
    {
    }
}