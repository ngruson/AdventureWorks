using MediatR;
using System.Collections.Generic;

namespace AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods
{
    public class GetShipMethodsQuery : IRequest<List<ShipMethod>>
    {
    }
}