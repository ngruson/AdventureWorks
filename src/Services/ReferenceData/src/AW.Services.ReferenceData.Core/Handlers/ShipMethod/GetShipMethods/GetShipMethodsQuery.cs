using Ardalis.Result;
using MediatR;

namespace AW.Services.ReferenceData.Core.Handlers.ShipMethod.GetShipMethods;

public class GetShipMethodsQuery : IRequest<Result<List<ShipMethod>>>
{
}
